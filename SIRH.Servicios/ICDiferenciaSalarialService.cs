using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SIRH.DTO;

namespace SIRH.Servicios
{
    // NOTA: puede usar el comando "Cambiar nombre" del menú "Refactorizar" para cambiar el nombre de interfaz "ICDiferenciaSalarialService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICDiferenciaSalarialService
    {
        /*              Diferencia Salarial                      */

        [OperationContract]
        CBaseDTO GuardarDiferenciaSalarial(CDiferenciaSalarialDTO item);

        [OperationContract]
        CBaseDTO BuscarDiferenciaSalarial(int PK_DiferenciaSalarial);

        [OperationContract]
        List<CBaseDTO> BuscarDiferenciasSalarialesPorPuesto(string codPuesto);

        [OperationContract]
        List<CBaseDTO> BuscarDiferenciasSalarialesPorFuncionario(string cedula);

        [OperationContract]
        List<CBaseDTO> ListarComponentesSalariales();

        /*              Detalle Diferencia Salarial              */

        [OperationContract]
        CBaseDTO GuardarDetalleDiferenciaSalarial(CDetalleDiferenciaSalarialDTO item);

        [OperationContract]
        CBaseDTO BuscarDetalleDiferenciaSalarial(int PK_DetalleDiferenciaSalarial);

        [OperationContract]
        List<CBaseDTO> FiltrarDiferenciasSalariales(CDiferenciaSalarialDTO filtro, DateTime? inicial, DateTime? final);

        [OperationContract]
        List<CBaseDTO> BuscarDetallesPorDiferenciaSalarial(int PK_DiferenciaSalarial);

        /*              Detalle Rubro                            */


        [OperationContract]
        CBaseDTO GuardarDetalleRubro(CDetalleRubroDTO item);

        [OperationContract]
        CBaseDTO BuscarDetalleRubro(int PK_DetalleRubro);

        [OperationContract]
        List<CBaseDTO> ObtenerCatalogoRubrosDeduccionPorDetalleDiferencia(int PK_DetalleDiferenciaSalarial);

        /*              Porcentaje Renta                         */

        [OperationContract]
        CBaseDTO GuardarPorcentajeRenta(CPorcentajeRentaDTO porcRenta);

        [OperationContract]
        List<CBaseDTO> BuscarPorcentajeRenta(DateTime fecha);

        /*              Porcentaje Salario Escolar               */

        [OperationContract]
        CBaseDTO GuardarPorcentajeSalarioEscolar(CPorcentajeSalarioEscolarDTO porcEscolar);

        [OperationContract]
        CBaseDTO BuscarPorcentajeSalarioEscolar(DateTime fecha);

        /*              Detalle Deducciones                      */

        [OperationContract]
        CBaseDTO GuardarDetalleDeducciones(CDetalleDeduccionesDTO item);

        [OperationContract]
        CBaseDTO BuscarDetalleDeducciones(int PK_DetalleDeducciones);

        [OperationContract]
        List<CBaseDTO> BuscarDetalleDeduccionPorDetalleDiferenciaSalarial(int PK_DetalleDiferenciaSalarial);

        /*              Catalogo Tipo Deduccion                  */

        [OperationContract]
        CBaseDTO GuardarCatalogoTipoDeduccion(CCatalogoTipoDeduccionDTO item);

        [OperationContract]
        CBaseDTO BuscarCatalogoTipoDeduccion(int PK_CatalogoTipoDeduccion);

        [OperationContract]
        List<List<CBaseDTO>> BuscarRubrosDeducciones(DateTime fecha, int PK_TipoDeduccion);

        /*              Rubro Deduccion                          */

        [OperationContract]
        CBaseDTO GuardarRubroDeduccion(CRubroDeduccionDTO item);

        [OperationContract]
        CBaseDTO BuscarRubroDeduccion(int PK_RubroDeduccion);

        [OperationContract]
        List<CBaseDTO> BuscarDetallesPorCatalogoTipoDeduccion(int PK_CatalogoTipoDeduccion);

        /*              Catalogo Rubro Deduccion                 */

        [OperationContract]
        CBaseDTO GuardarCatalogoRubroDeduccion(CCatalogoRubroDeduccionDTO rubro);

        [OperationContract]
        CBaseDTO BuscarCatalogoRubroDeduccion(int PK_CatalogoRubroDeduccion);

        [OperationContract]
        List<CBaseDTO> ObtenerCatalogoRubrosDeduccion();
    }
}
