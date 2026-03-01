using System;
using System.Collections.Generic;
using System.Linq;
using AGDevX.Exceptions;
using AGDevX.Objects;
using Xunit;

namespace AGDevX.Tests.Exceptions;

public sealed class ExceptionDetailTests
{
    private const string _applicationStartupExceptionMessage = "The application failed to start";
    private const string _extensionMethodExceptionMessage = "Unhandled scenario";
    private const string _nullReferenceExceptionMessage = "Dude, the object was null.";

    private static readonly List<string> _assemblyPrefixes = new() { "AGDevX", "JMC", "RD" };

    public class When_calling_GetExceptionDetail
    {
        public class And_including_stack_frames_and_filtering_stack_frames_for_a_generic_method
        {
            //-- Scoped to this nested class so the static local function can access it without a closure
            private static ApplicationStartupException? _applicationStartupException;

            [Fact]
            public void Then_return_filtered_stack_frames()
            {
                //-- Arrange
                var nullReferenceException = new NullReferenceException(_nullReferenceExceptionMessage);
                _applicationStartupException = new ApplicationStartupException(_applicationStartupExceptionMessage, nullReferenceException);

                var includeStackFrames = true;
                var filterStackFrames = true;

                //-- Act
                try
                {
                    static void localF<T>()
                    {
                        throw _applicationStartupException!;
                    }

                    localF<object>();
                }
                catch (ApplicationStartupException appStartEx)
                {
                    var exceptionDetail = appStartEx.GetExceptionDetail(includeStackFrames, filterStackFrames, _assemblyPrefixes);

                    //-- Assert
                    Assert.True(exceptionDetail.Code.Equals(_applicationStartupException.Code));
                    Assert.True(exceptionDetail.Message.Equals(_applicationStartupException.Message));
                    Assert.True(exceptionDetail.StackFrames.IsNotNull());
                    Assert.IsType<int>(exceptionDetail.StackFrames.First().LineNumber);
                    Assert.True(exceptionDetail.StackFrames.First().Method!.Equals("localF<T>()"));
                    Assert.Contains("AGDevX.Tests.dll", exceptionDetail.StackFrames.First().AssemblyFile);
                    Assert.Contains("AGDevX.Tests", exceptionDetail.StackFrames.First().AssemblyName);
                    Assert.True(exceptionDetail.StackFrames.First().Class!.Equals("AGDevX.Tests.Exceptions.ExceptionDetailTests+When_calling_GetExceptionDetail+And_including_stack_frames_and_filtering_stack_frames_for_a_generic_method"));
                    Assert.Contains("ExceptionDetailTests.cs", exceptionDetail.StackFrames.First().CodeFile);
                    Assert.True(exceptionDetail.InnerException.IsNotNull());
                }
            }
        }

        public class And_including_stack_frames_and_filtering_stack_frames_for_a_non_generic_method
        {
            [Fact]
            public void Then_return_filtered_stack_frames()
            {
                //-- Arrange
                var nullReferenceException = new NullReferenceException(_nullReferenceExceptionMessage);
                var applicationStartupException = new ApplicationStartupException(_applicationStartupExceptionMessage, nullReferenceException);

                var includeStackFrames = true;
                var filterStackFrames = true;

                //-- Act
                try
                {
                    throw applicationStartupException;
                }
                catch (ApplicationStartupException appStartEx)
                {
                    var exceptionDetail = appStartEx.GetExceptionDetail(includeStackFrames, filterStackFrames, _assemblyPrefixes);

                    //-- Assert
                    Assert.True(exceptionDetail.Code.Equals(applicationStartupException.Code));
                    Assert.True(exceptionDetail.Message.Equals(applicationStartupException.Message));
                    Assert.True(exceptionDetail.StackFrames.IsNotNull());
                    Assert.IsType<int>(exceptionDetail.StackFrames.First().LineNumber);
                    Assert.True(exceptionDetail.StackFrames.First().Method!.Equals("Then_return_filtered_stack_frames()"));
                    Assert.Contains("AGDevX.Tests.dll", exceptionDetail.StackFrames.First().AssemblyFile);
                    Assert.Contains("AGDevX.Tests", exceptionDetail.StackFrames.First().AssemblyName);
                    Assert.True(exceptionDetail.StackFrames.First().Class!.Equals("AGDevX.Tests.Exceptions.ExceptionDetailTests+When_calling_GetExceptionDetail+And_including_stack_frames_and_filtering_stack_frames_for_a_non_generic_method"));
                    Assert.Contains("ExceptionDetailTests.cs", exceptionDetail.StackFrames.First().CodeFile);
                    Assert.True(exceptionDetail.InnerException.IsNotNull());
                }
            }
        }

        public class And_including_stack_frames_and_filtering_stack_frames_for_a_method_name_ending_in_a_symbol
        {
            [Fact]
            public void GetExceptionDetail_()
            {
                //-- Arrange
                var extensionMethodException = new ExtensionMethodException(_extensionMethodExceptionMessage);
                var applicationStartupException = new ApplicationStartupException(_extensionMethodExceptionMessage, extensionMethodException);

                var includeStackFrames = true;
                var filterStackFrames = true;

                //-- Act
                var exceptionDetail = applicationStartupException.GetExceptionDetail(includeStackFrames, filterStackFrames);

                //-- Assert
                Assert.True(exceptionDetail.Code.Equals(applicationStartupException.Code));
                Assert.True(exceptionDetail.Message.Equals(applicationStartupException.Message));
                Assert.True(exceptionDetail.StackFrames.IsNotNull());
                Assert.True(exceptionDetail.InnerException.IsNotNull());
            }
        }

        public class And_not_including_stack_frames
        {
            [Fact]
            public void Then_do_not_return_stack_frames()
            {
                //-- Arrange
                var nullReferenceException = new NullReferenceException(_nullReferenceExceptionMessage);
                var applicationStartupException = new ApplicationStartupException(_applicationStartupExceptionMessage, nullReferenceException);

                var includeStackFrames = false;
                var filterStackFrames = true;

                //-- Act
                try
                {
                    throw applicationStartupException;
                }
                catch (ApplicationStartupException appStartEx)
                {
                    var exceptionDetail = appStartEx.GetExceptionDetail(includeStackFrames, filterStackFrames, _assemblyPrefixes);

                    //-- Assert
                    Assert.True(exceptionDetail.Code.Equals(applicationStartupException.Code));
                    Assert.True(exceptionDetail.Message.Equals(applicationStartupException.Message));
                    Assert.True(exceptionDetail.StackFrames.IsNull());
                    Assert.True(exceptionDetail.InnerException.IsNotNull());
                }
            }
        }

        public class And_called_on_a_plain_Exception
        {
            [Fact]
            public void Then_return_exception_detail_with_provided_code()
            {
                //-- Arrange
                var code = "CUSTOM_CODE";
                var message = "Something went wrong";
                var exception = new InvalidOperationException(message);

                //-- Act
                ExceptionDetail exceptionDetail;
                try
                {
                    throw exception;
                }
                catch (Exception ex)
                {
                    exceptionDetail = ex.GetExceptionDetail(code);
                }

                //-- Assert
                Assert.Equal(code, exceptionDetail.Code);
                Assert.Equal(message, exceptionDetail.Message);
                Assert.NotNull(exceptionDetail.StackFrames);
                Assert.Null(exceptionDetail.InnerException);
            }
        }
    }
}
