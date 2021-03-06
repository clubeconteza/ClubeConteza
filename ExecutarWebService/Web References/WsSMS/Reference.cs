﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace ExecutarWebService.WsSMS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SMSSoap", Namespace="http://tempuri.org/")]
    public partial class SMS : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ListarNovosSmsRecebidosOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnvioUnicoSmsOperationCompleted;
        
        private System.Threading.SendOrPostCallback EnvioVariosSmsOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultarSmsRecebidosPorPeriodoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SMS() {
            this.Url = global::ExecutarWebService.Properties.Settings.Default.ExecutarWebService_WsSMS_SMS;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ListarNovosSmsRecebidosCompletedEventHandler ListarNovosSmsRecebidosCompleted;
        
        /// <remarks/>
        public event EnvioUnicoSmsCompletedEventHandler EnvioUnicoSmsCompleted;
        
        /// <remarks/>
        public event EnvioVariosSmsCompletedEventHandler EnvioVariosSmsCompleted;
        
        /// <remarks/>
        public event ConsultarSmsRecebidosPorPeriodoCompletedEventHandler ConsultarSmsRecebidosPorPeriodoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ListarNovosSmsRecebidos", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ListarNovosSmsRecebidos() {
            object[] results = this.Invoke("ListarNovosSmsRecebidos", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ListarNovosSmsRecebidosAsync() {
            this.ListarNovosSmsRecebidosAsync(null);
        }
        
        /// <remarks/>
        public void ListarNovosSmsRecebidosAsync(object userState) {
            if ((this.ListarNovosSmsRecebidosOperationCompleted == null)) {
                this.ListarNovosSmsRecebidosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnListarNovosSmsRecebidosOperationCompleted);
            }
            this.InvokeAsync("ListarNovosSmsRecebidos", new object[0], this.ListarNovosSmsRecebidosOperationCompleted, userState);
        }
        
        private void OnListarNovosSmsRecebidosOperationCompleted(object arg) {
            if ((this.ListarNovosSmsRecebidosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ListarNovosSmsRecebidosCompleted(this, new ListarNovosSmsRecebidosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnvioUnicoSms", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string EnvioUnicoSms(EnviaSmsController sms) {
            object[] results = this.Invoke("EnvioUnicoSms", new object[] {
                        sms});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void EnvioUnicoSmsAsync(EnviaSmsController sms) {
            this.EnvioUnicoSmsAsync(sms, null);
        }
        
        /// <remarks/>
        public void EnvioUnicoSmsAsync(EnviaSmsController sms, object userState) {
            if ((this.EnvioUnicoSmsOperationCompleted == null)) {
                this.EnvioUnicoSmsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnvioUnicoSmsOperationCompleted);
            }
            this.InvokeAsync("EnvioUnicoSms", new object[] {
                        sms}, this.EnvioUnicoSmsOperationCompleted, userState);
        }
        
        private void OnEnvioUnicoSmsOperationCompleted(object arg) {
            if ((this.EnvioUnicoSmsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnvioUnicoSmsCompleted(this, new EnvioUnicoSmsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnvioVariosSms", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string EnvioVariosSms(EnviaSmsMultiController sms) {
            object[] results = this.Invoke("EnvioVariosSms", new object[] {
                        sms});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void EnvioVariosSmsAsync(EnviaSmsMultiController sms) {
            this.EnvioVariosSmsAsync(sms, null);
        }
        
        /// <remarks/>
        public void EnvioVariosSmsAsync(EnviaSmsMultiController sms, object userState) {
            if ((this.EnvioVariosSmsOperationCompleted == null)) {
                this.EnvioVariosSmsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnvioVariosSmsOperationCompleted);
            }
            this.InvokeAsync("EnvioVariosSms", new object[] {
                        sms}, this.EnvioVariosSmsOperationCompleted, userState);
        }
        
        private void OnEnvioVariosSmsOperationCompleted(object arg) {
            if ((this.EnvioVariosSmsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnvioVariosSmsCompleted(this, new EnvioVariosSmsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ConsultarSmsRecebidosPorPeriodo", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ConsultarSmsRecebidosPorPeriodo(System.DateTime dataInicio, System.DateTime dataFinal) {
            object[] results = this.Invoke("ConsultarSmsRecebidosPorPeriodo", new object[] {
                        dataInicio,
                        dataFinal});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultarSmsRecebidosPorPeriodoAsync(System.DateTime dataInicio, System.DateTime dataFinal) {
            this.ConsultarSmsRecebidosPorPeriodoAsync(dataInicio, dataFinal, null);
        }
        
        /// <remarks/>
        public void ConsultarSmsRecebidosPorPeriodoAsync(System.DateTime dataInicio, System.DateTime dataFinal, object userState) {
            if ((this.ConsultarSmsRecebidosPorPeriodoOperationCompleted == null)) {
                this.ConsultarSmsRecebidosPorPeriodoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultarSmsRecebidosPorPeriodoOperationCompleted);
            }
            this.InvokeAsync("ConsultarSmsRecebidosPorPeriodo", new object[] {
                        dataInicio,
                        dataFinal}, this.ConsultarSmsRecebidosPorPeriodoOperationCompleted, userState);
        }
        
        private void OnConsultarSmsRecebidosPorPeriodoOperationCompleted(object arg) {
            if ((this.ConsultarSmsRecebidosPorPeriodoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultarSmsRecebidosPorPeriodoCompleted(this, new ConsultarSmsRecebidosPorPeriodoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class EnviaSmsController {
        
        private MensagemSmsController mensagemSmsField;
        
        /// <remarks/>
        public MensagemSmsController MensagemSms {
            get {
                return this.mensagemSmsField;
            }
            set {
                this.mensagemSmsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class MensagemSmsController {
        
        private string remetenteField;
        
        private long numeroCelularField;
        
        private System.Nullable<System.DateTime> dataMensagemEnviaField;
        
        private string mensagemField;
        
        private System.Nullable<long> idMensagemField;
        
        /// <remarks/>
        public string Remetente {
            get {
                return this.remetenteField;
            }
            set {
                this.remetenteField = value;
            }
        }
        
        /// <remarks/>
        public long NumeroCelular {
            get {
                return this.numeroCelularField;
            }
            set {
                this.numeroCelularField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> DataMensagemEnvia {
            get {
                return this.dataMensagemEnviaField;
            }
            set {
                this.dataMensagemEnviaField = value;
            }
        }
        
        /// <remarks/>
        public string Mensagem {
            get {
                return this.mensagemField;
            }
            set {
                this.mensagemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<long> IdMensagem {
            get {
                return this.idMensagemField;
            }
            set {
                this.idMensagemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class MensagemVariosSmsController {
        
        private string remetenteField;
        
        private long numeroCelularField;
        
        private System.Nullable<System.DateTime> dataMensagemEnviaField;
        
        private string mensagemField;
        
        private System.Nullable<long> idMensagemField;
        
        /// <remarks/>
        public string Remetente {
            get {
                return this.remetenteField;
            }
            set {
                this.remetenteField = value;
            }
        }
        
        /// <remarks/>
        public long NumeroCelular {
            get {
                return this.numeroCelularField;
            }
            set {
                this.numeroCelularField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<System.DateTime> DataMensagemEnvia {
            get {
                return this.dataMensagemEnviaField;
            }
            set {
                this.dataMensagemEnviaField = value;
            }
        }
        
        /// <remarks/>
        public string Mensagem {
            get {
                return this.mensagemField;
            }
            set {
                this.mensagemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<long> IdMensagem {
            get {
                return this.idMensagemField;
            }
            set {
                this.idMensagemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class EnviaSmsListaController {
        
        private MensagemVariosSmsController[] mensagemVariosSmsField;
        
        /// <remarks/>
        public MensagemVariosSmsController[] MensagemVariosSms {
            get {
                return this.mensagemVariosSmsField;
            }
            set {
                this.mensagemVariosSmsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class EnviaSmsMultiController {
        
        private EnviaSmsListaController enviaSmsListaField;
        
        /// <remarks/>
        public EnviaSmsListaController EnviaSmsLista {
            get {
                return this.enviaSmsListaField;
            }
            set {
                this.enviaSmsListaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ListarNovosSmsRecebidosCompletedEventHandler(object sender, ListarNovosSmsRecebidosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListarNovosSmsRecebidosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ListarNovosSmsRecebidosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void EnvioUnicoSmsCompletedEventHandler(object sender, EnvioUnicoSmsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnvioUnicoSmsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnvioUnicoSmsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void EnvioVariosSmsCompletedEventHandler(object sender, EnvioVariosSmsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnvioVariosSmsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnvioVariosSmsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ConsultarSmsRecebidosPorPeriodoCompletedEventHandler(object sender, ConsultarSmsRecebidosPorPeriodoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultarSmsRecebidosPorPeriodoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultarSmsRecebidosPorPeriodoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591