﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Szcg.Web.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://service.boc.com/", ConfigurationName="ServiceReference1.CobSecurityServiceDelegate")]
    public interface CobSecurityServiceDelegate {
        
        // CODEGEN: 参数“arg0”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Szcg.Web.ServiceReference1.mainResponse main(Szcg.Web.ServiceReference1.mainRequest request);
        
        // CODEGEN: 参数“return”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        Szcg.Web.ServiceReference1.downloadFileResponse downloadFile(Szcg.Web.ServiceReference1.downloadFileRequest request);
        
        // CODEGEN: 参数“return”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        Szcg.Web.ServiceReference1.EncryptPasswordResponse EncryptPassword(Szcg.Web.ServiceReference1.EncryptPasswordRequest request);
        
        // CODEGEN: 参数“return”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        Szcg.Web.ServiceReference1.EncryptFileResponse EncryptFile(Szcg.Web.ServiceReference1.EncryptFileRequest request);
        
        // CODEGEN: 参数“return”需要其他方案信息，使用参数模式无法捕获这些信息。特定特性为“System.Xml.Serialization.XmlElementAttribute”。
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        Szcg.Web.ServiceReference1.DecryptFileResponse DecryptFile(Szcg.Web.ServiceReference1.DecryptFileRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="main", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class mainRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("arg0", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string[] arg0;
        
        public mainRequest() {
        }
        
        public mainRequest(string[] arg0) {
            this.arg0 = arg0;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="mainResponse", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class mainResponse {
        
        public mainResponse() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="downloadFile", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class downloadFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string arg0;
        
        public downloadFileRequest() {
        }
        
        public downloadFileRequest(string arg0) {
            this.arg0 = arg0;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="downloadFileResponse", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class downloadFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)]
        public byte[] @return;
        
        public downloadFileResponse() {
        }
        
        public downloadFileResponse(byte[] @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="EncryptPassword", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class EncryptPasswordRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string arg0;
        
        public EncryptPasswordRequest() {
        }
        
        public EncryptPasswordRequest(string arg0) {
            this.arg0 = arg0;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="EncryptPasswordResponse", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class EncryptPasswordResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string @return;
        
        public EncryptPasswordResponse() {
        }
        
        public EncryptPasswordResponse(string @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="EncryptFile", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class EncryptFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string arg0;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)]
        public byte[] arg1;
        
        public EncryptFileRequest() {
        }
        
        public EncryptFileRequest(string arg0, byte[] arg1) {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="EncryptFileResponse", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class EncryptFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)]
        public byte[] @return;
        
        public EncryptFileResponse() {
        }
        
        public EncryptFileResponse(byte[] @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DecryptFile", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class DecryptFileRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string arg0;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)]
        public byte[] arg1;
        
        public DecryptFileRequest() {
        }
        
        public DecryptFileRequest(string arg0, byte[] arg1) {
            this.arg0 = arg0;
            this.arg1 = arg1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DecryptFileResponse", WrapperNamespace="http://service.boc.com/", IsWrapped=true)]
    public partial class DecryptFileResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.boc.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", IsNullable=true)]
        public byte[] @return;
        
        public DecryptFileResponse() {
        }
        
        public DecryptFileResponse(byte[] @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CobSecurityServiceDelegateChannel : Szcg.Web.ServiceReference1.CobSecurityServiceDelegate, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CobSecurityServiceDelegateClient : System.ServiceModel.ClientBase<Szcg.Web.ServiceReference1.CobSecurityServiceDelegate>, Szcg.Web.ServiceReference1.CobSecurityServiceDelegate {
        
        public CobSecurityServiceDelegateClient() {
        }
        
        public CobSecurityServiceDelegateClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CobSecurityServiceDelegateClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CobSecurityServiceDelegateClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CobSecurityServiceDelegateClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Szcg.Web.ServiceReference1.mainResponse Szcg.Web.ServiceReference1.CobSecurityServiceDelegate.main(Szcg.Web.ServiceReference1.mainRequest request) {
            return base.Channel.main(request);
        }
        
        public void main(string[] arg0) {
            Szcg.Web.ServiceReference1.mainRequest inValue = new Szcg.Web.ServiceReference1.mainRequest();
            inValue.arg0 = arg0;
            Szcg.Web.ServiceReference1.mainResponse retVal = ((Szcg.Web.ServiceReference1.CobSecurityServiceDelegate)(this)).main(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Szcg.Web.ServiceReference1.downloadFileResponse Szcg.Web.ServiceReference1.CobSecurityServiceDelegate.downloadFile(Szcg.Web.ServiceReference1.downloadFileRequest request) {
            return base.Channel.downloadFile(request);
        }
        
        public byte[] downloadFile(string arg0) {
            Szcg.Web.ServiceReference1.downloadFileRequest inValue = new Szcg.Web.ServiceReference1.downloadFileRequest();
            inValue.arg0 = arg0;
            Szcg.Web.ServiceReference1.downloadFileResponse retVal = ((Szcg.Web.ServiceReference1.CobSecurityServiceDelegate)(this)).downloadFile(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Szcg.Web.ServiceReference1.EncryptPasswordResponse Szcg.Web.ServiceReference1.CobSecurityServiceDelegate.EncryptPassword(Szcg.Web.ServiceReference1.EncryptPasswordRequest request) {
            return base.Channel.EncryptPassword(request);
        }
        
        public string EncryptPassword(string arg0) {
            Szcg.Web.ServiceReference1.EncryptPasswordRequest inValue = new Szcg.Web.ServiceReference1.EncryptPasswordRequest();
            inValue.arg0 = arg0;
            Szcg.Web.ServiceReference1.EncryptPasswordResponse retVal = ((Szcg.Web.ServiceReference1.CobSecurityServiceDelegate)(this)).EncryptPassword(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Szcg.Web.ServiceReference1.EncryptFileResponse Szcg.Web.ServiceReference1.CobSecurityServiceDelegate.EncryptFile(Szcg.Web.ServiceReference1.EncryptFileRequest request) {
            return base.Channel.EncryptFile(request);
        }
        
        public byte[] EncryptFile(string arg0, byte[] arg1) {
            Szcg.Web.ServiceReference1.EncryptFileRequest inValue = new Szcg.Web.ServiceReference1.EncryptFileRequest();
            inValue.arg0 = arg0;
            inValue.arg1 = arg1;
            Szcg.Web.ServiceReference1.EncryptFileResponse retVal = ((Szcg.Web.ServiceReference1.CobSecurityServiceDelegate)(this)).EncryptFile(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Szcg.Web.ServiceReference1.DecryptFileResponse Szcg.Web.ServiceReference1.CobSecurityServiceDelegate.DecryptFile(Szcg.Web.ServiceReference1.DecryptFileRequest request) {
            return base.Channel.DecryptFile(request);
        }
        
        public byte[] DecryptFile(string arg0, byte[] arg1) {
            Szcg.Web.ServiceReference1.DecryptFileRequest inValue = new Szcg.Web.ServiceReference1.DecryptFileRequest();
            inValue.arg0 = arg0;
            inValue.arg1 = arg1;
            Szcg.Web.ServiceReference1.DecryptFileResponse retVal = ((Szcg.Web.ServiceReference1.CobSecurityServiceDelegate)(this)).DecryptFile(inValue);
            return retVal.@return;
        }
    }
}