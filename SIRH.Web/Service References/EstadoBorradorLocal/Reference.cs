﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIRH.Web.EstadoBorradorLocal {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="EstadoBorradorLocal.ICEstadoBorradorService")]
    public interface ICEstadoBorradorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICEstadoBorradorService/RetornarEstados", ReplyAction="http://tempuri.org/ICEstadoBorradorService/RetornarEstadosResponse")]
        SIRH.DTO.CBaseDTO[] RetornarEstados();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICEstadoBorradorService/RetornarEstados", ReplyAction="http://tempuri.org/ICEstadoBorradorService/RetornarEstadosResponse")]
        System.Threading.Tasks.Task<SIRH.DTO.CBaseDTO[]> RetornarEstadosAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICEstadoBorradorService/ObtenerEstado", ReplyAction="http://tempuri.org/ICEstadoBorradorService/ObtenerEstadoResponse")]
        SIRH.DTO.CBaseDTO[] ObtenerEstado(int codigo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICEstadoBorradorService/ObtenerEstado", ReplyAction="http://tempuri.org/ICEstadoBorradorService/ObtenerEstadoResponse")]
        System.Threading.Tasks.Task<SIRH.DTO.CBaseDTO[]> ObtenerEstadoAsync(int codigo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICEstadoBorradorServiceChannel : SIRH.Web.EstadoBorradorLocal.ICEstadoBorradorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CEstadoBorradorServiceClient : System.ServiceModel.ClientBase<SIRH.Web.EstadoBorradorLocal.ICEstadoBorradorService>, SIRH.Web.EstadoBorradorLocal.ICEstadoBorradorService {
        
        public CEstadoBorradorServiceClient() {
        }
        
        public CEstadoBorradorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CEstadoBorradorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CEstadoBorradorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CEstadoBorradorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SIRH.DTO.CBaseDTO[] RetornarEstados() {
            return base.Channel.RetornarEstados();
        }
        
        public System.Threading.Tasks.Task<SIRH.DTO.CBaseDTO[]> RetornarEstadosAsync() {
            return base.Channel.RetornarEstadosAsync();
        }
        
        public SIRH.DTO.CBaseDTO[] ObtenerEstado(int codigo) {
            return base.Channel.ObtenerEstado(codigo);
        }
        
        public System.Threading.Tasks.Task<SIRH.DTO.CBaseDTO[]> ObtenerEstadoAsync(int codigo) {
            return base.Channel.ObtenerEstadoAsync(codigo);
        }
    }
}
