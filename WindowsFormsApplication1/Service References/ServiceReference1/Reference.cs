﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApplication1.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.WebService1Soap")]
    public interface WebService1Soap {
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 HelloWorldResult 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        WindowsFormsApplication1.ServiceReference1.HelloWorldResponse HelloWorld(WindowsFormsApplication1.ServiceReference1.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.HelloWorldResponse> HelloWorldAsync(WindowsFormsApplication1.ServiceReference1.HelloWorldRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 userName 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Show", ReplyAction="*")]
        WindowsFormsApplication1.ServiceReference1.ShowResponse Show(WindowsFormsApplication1.ServiceReference1.ShowRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Show", ReplyAction="*")]
        System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.ShowResponse> ShowAsync(WindowsFormsApplication1.ServiceReference1.ShowRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public WindowsFormsApplication1.ServiceReference1.HelloWorldRequestBody Body;
        
        public HelloWorldRequest() {
        }
        
        public HelloWorldRequest(WindowsFormsApplication1.ServiceReference1.HelloWorldRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody {
        
        public HelloWorldRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public WindowsFormsApplication1.ServiceReference1.HelloWorldResponseBody Body;
        
        public HelloWorldResponse() {
        }
        
        public HelloWorldResponse(WindowsFormsApplication1.ServiceReference1.HelloWorldResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody() {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult) {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ShowRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Show", Namespace="http://tempuri.org/", Order=0)]
        public WindowsFormsApplication1.ServiceReference1.ShowRequestBody Body;
        
        public ShowRequest() {
        }
        
        public ShowRequest(WindowsFormsApplication1.ServiceReference1.ShowRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ShowRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string userName;
        
        public ShowRequestBody() {
        }
        
        public ShowRequestBody(string userName) {
            this.userName = userName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ShowResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ShowResponse", Namespace="http://tempuri.org/", Order=0)]
        public WindowsFormsApplication1.ServiceReference1.ShowResponseBody Body;
        
        public ShowResponse() {
        }
        
        public ShowResponse(WindowsFormsApplication1.ServiceReference1.ShowResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ShowResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ShowResult;
        
        public ShowResponseBody() {
        }
        
        public ShowResponseBody(string ShowResult) {
            this.ShowResult = ShowResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebService1SoapChannel : WindowsFormsApplication1.ServiceReference1.WebService1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebService1SoapClient : System.ServiceModel.ClientBase<WindowsFormsApplication1.ServiceReference1.WebService1Soap>, WindowsFormsApplication1.ServiceReference1.WebService1Soap {
        
        public WebService1SoapClient() {
        }
        
        public WebService1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApplication1.ServiceReference1.HelloWorldResponse WindowsFormsApplication1.ServiceReference1.WebService1Soap.HelloWorld(WindowsFormsApplication1.ServiceReference1.HelloWorldRequest request) {
            return base.Channel.HelloWorld(request);
        }
        
        public string HelloWorld() {
            WindowsFormsApplication1.ServiceReference1.HelloWorldRequest inValue = new WindowsFormsApplication1.ServiceReference1.HelloWorldRequest();
            inValue.Body = new WindowsFormsApplication1.ServiceReference1.HelloWorldRequestBody();
            WindowsFormsApplication1.ServiceReference1.HelloWorldResponse retVal = ((WindowsFormsApplication1.ServiceReference1.WebService1Soap)(this)).HelloWorld(inValue);
            return retVal.Body.HelloWorldResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.HelloWorldResponse> WindowsFormsApplication1.ServiceReference1.WebService1Soap.HelloWorldAsync(WindowsFormsApplication1.ServiceReference1.HelloWorldRequest request) {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.HelloWorldResponse> HelloWorldAsync() {
            WindowsFormsApplication1.ServiceReference1.HelloWorldRequest inValue = new WindowsFormsApplication1.ServiceReference1.HelloWorldRequest();
            inValue.Body = new WindowsFormsApplication1.ServiceReference1.HelloWorldRequestBody();
            return ((WindowsFormsApplication1.ServiceReference1.WebService1Soap)(this)).HelloWorldAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WindowsFormsApplication1.ServiceReference1.ShowResponse WindowsFormsApplication1.ServiceReference1.WebService1Soap.Show(WindowsFormsApplication1.ServiceReference1.ShowRequest request) {
            return base.Channel.Show(request);
        }
        
        public string Show(string userName) {
            WindowsFormsApplication1.ServiceReference1.ShowRequest inValue = new WindowsFormsApplication1.ServiceReference1.ShowRequest();
            inValue.Body = new WindowsFormsApplication1.ServiceReference1.ShowRequestBody();
            inValue.Body.userName = userName;
            WindowsFormsApplication1.ServiceReference1.ShowResponse retVal = ((WindowsFormsApplication1.ServiceReference1.WebService1Soap)(this)).Show(inValue);
            return retVal.Body.ShowResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.ShowResponse> WindowsFormsApplication1.ServiceReference1.WebService1Soap.ShowAsync(WindowsFormsApplication1.ServiceReference1.ShowRequest request) {
            return base.Channel.ShowAsync(request);
        }
        
        public System.Threading.Tasks.Task<WindowsFormsApplication1.ServiceReference1.ShowResponse> ShowAsync(string userName) {
            WindowsFormsApplication1.ServiceReference1.ShowRequest inValue = new WindowsFormsApplication1.ServiceReference1.ShowRequest();
            inValue.Body = new WindowsFormsApplication1.ServiceReference1.ShowRequestBody();
            inValue.Body.userName = userName;
            return ((WindowsFormsApplication1.ServiceReference1.WebService1Soap)(this)).ShowAsync(inValue);
        }
    }
}
