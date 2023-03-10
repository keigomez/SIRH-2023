using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SIRH.DTO;
using SIRH.Logica;

namespace SIRH.Servicios
{
    // NOTE: If you change the interface name "ICComponentePresupuestarioService" here, you must also update the reference to "ICComponentePresupuestarioService" in App.config.
    [ServiceContract]
    public interface ICComponentePresupuestarioService
    {
        [OperationContract]
        CBaseDTO GuardarComponentePresupuestario(CProgramaDTO programa, CObjetoGastoDTO objetoGasto,
                                                   CCatMovimientoPresupuestoDTO tipo, CComponentePresupuestarioDTO componente);

        [OperationContract]
        CBaseDTO AgregarDecretoComponentePresupuestario(CProgramaDTO programa, CObjetoGastoDTO objetoGasto,
                                                    CCatMovimientoPresupuestoDTO tipo, CComponentePresupuestarioDTO componente);

        [OperationContract]
        CBaseDTO EditarComponentePresupuestario(CComponentePresupuestarioDTO componente);


        [OperationContract]
        List<CBaseDTO> ListarMovimientosPresupuesto(string anno);

        [OperationContract]
        List<CBaseDTO> ObtenerMovimientoPresupuesto(int idMovimiento);

        [OperationContract]
        List<CBaseDTO> DescargarProgramas();

        [OperationContract]
        List<CBaseDTO> DescargarObjetosGasto();

        [OperationContract]
        List<CBaseDTO> DescargarCatMovimientoPresupuesto();

        [OperationContract]
        List<CBaseDTO> BuscarPresupuestos(CComponentePresupuestarioDTO componente, List<string> anios, List<int> programas);

        [OperationContract]
        List<CBaseDTO> ListarComponentePresupuestario();

        [OperationContract]
        List<CBaseDTO> ListarProgramas();


        [OperationContract]
        List<CBaseDTO> ListarObjetoGasto();
        

        [OperationContract]
        CBaseDTO ObtenerDecreto(int codDecreto);
    }
}
