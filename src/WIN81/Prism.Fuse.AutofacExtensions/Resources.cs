using Windows.ApplicationModel.Resources;

namespace Microsoft.Practices.Prism.Properties
{
    internal static class Resources
    {
        public static string NullLoggerFacadeException
        {
            get
            {
                return GetString("NullLoggerFacadeException");
            }
        }

        public static string NullSuspensionManagerException
        {
            get
            {
                return GetString("NullSuspensionManagerException");
            }
        }

        public static string InitializingShell
        {
            get
            {
                return GetString("InitializingShell");
            }
        }

        public static string CreatingContainer
        {
            get
            {
                return GetString("CreatingContainer");
            }
        }

        public static string NullContainerException
        {
            get
            {
                return GetString("NullContainerException");
            }
        }

        public static string ConfiguringIocContainer
        {
            get
            {
                return GetString("ConfiguringIocContainer");
            }
        }

        public static string ConfiguringServiceLocatorSingleton
        {
            get
            {
                return GetString("ConfiguringServiceLocatorSingleton");
            }
        }

        public static string BootstrapperSequenceCompleted
        {
            get
            {
                return GetString("BootstrapperSequenceCompleted");
            }
        }

        public static string TypeMappingAlreadyRegistered
        {
            get
            {
                return GetString("TypeMappingAlreadyRegistered");
            }
        }

        public static string DelegateCommandDelegatesCannotBeNull
        {
            get
            {
                return GetString("DelegateCommandDelegatesCannotBeNull");
            }
        }

        public static string DelegateCommandInvalidGenericPayloadType
        {
            get
            {
                return GetString("DelegateCommandInvalidGenericPayloadType");
            }
        }

        public static string MemberExpression
        {
            get
            {
                return GetString("MemberExpression");
            }
        }

        public static string PropertyExpression
        {
            get
            {
                return GetString("PropertyExpression");
            }
        }

        public static string ConstantExpression
        {
            get
            {
                return GetString("ConstantExpression");
            }
        }

        public static string CannotRegisterCompositeCommandInItself
        {
            get
            {
                return GetString("CannotRegisterCompositeCommandInItself");
            }
        }

        public static string CannotRegisterSameCommandTwice
        {
            get
            {
                return GetString("CannotRegisterSameCommandTwice");
            }
        }

        public static string PropertySupportNotMemberAccessExpressionException
        {
            get
            {
                return GetString("PropertySupport_NotMemberAccessExpression_Exception");
            }
        }

        public static string PropertySupportExpressionNotPropertyException
        {
            get
            {
                return GetString("PropertySupport_ExpressionNotProperty_Exception");
            }
        }

        public static string PropertySupportStaticExpressionException
        {
            get
            {
                return GetString("PropertySupport_StaticExpression_Exception");
            }
        }

        public static string GetString(string resourceName)
        {
            return ResourceLoader.GetForViewIndependentUse("Prism.Fuse.AutofacExtensions/Resources").GetString(resourceName);
        }
    }
}
