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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "CDiferenciaSalarialService" en el código y en el archivo de configuración a la vez.
    public class CDiferenciaSalarialService : ICDiferenciaSalarialService
    {
        /*              Diferencia Salarial                      */
        public CBaseDTO GuardarDiferenciaSalarial(CDiferenciaSalarialDTO item)
        {
            return new CDiferenciaSalarialL().GuardarDiferenciaSalarial(item);
        }

        public CBaseDTO BuscarDiferenciaSalarial(int PK_DiferenciaSalarial)
        {
            return new CDiferenciaSalarialL().BuscarDiferenciaSalarial(PK_DiferenciaSalarial);
        }

        public List<CBaseDTO> BuscarDiferenciasSalarialesPorPuesto(string codPuesto)
        {
            return new CDiferenciaSalarialL().BuscarDiferenciasSalarialesPorPuesto(codPuesto);
        }

        public List<CBaseDTO> FiltrarDiferenciasSalariales(CDiferenciaSalarialDTO filtro, DateTime? inicial, DateTime? final)
        {
            return new CDiferenciaSalarialL().FiltrarDiferenciasSalariales(filtro, inicial, final);
        }

        public List<CBaseDTO> BuscarDiferenciasSalarialesPorFuncionario(string cedula)
        {
            return new CDiferenciaSalarialL().BuscarDiferenciasSalarialesPorFuncionario(cedula);
        }

        public List<CBaseDTO> ListarComponentesSalariales()
        {
            return new CDiferenciaSalarialL().ListarComponentesSalariales();
        }

        /*              Detalle Diferencia Salarial              */

        public CBaseDTO GuardarDetalleDiferenciaSalarial(CDetalleDiferenciaSalarialDTO item)
        {
            return new CDetalleDiferenciaSalarialL().GuardarDetalleDiferenciaSalarial(item);
        }

        public CBaseDTO BuscarDetalleDiferenciaSalarial(int PK_DetalleDiferenciaSalarial)
        {
            return new CDetalleDiferenciaSalarialL().BuscarDetalleDiferenciaSalarial(PK_DetalleDiferenciaSalarial);
        }

        public List<CBaseDTO> BuscarDetallesPorDiferenciaSalarial(int PK_DiferenciaSalarial)
        {
            return new CDetalleDiferenciaSalarialL().BuscarDetallesPorDiferenciaSalarial(PK_DiferenciaSalarial);
        }

        /*              Detalle Rubro                            */

        public CBaseDTO GuardarDetalleRubro(CDetalleRubroDTO item)
        {
            return new CDetalleRubroL().GuardarDetalleRubro(item);
        }

        public CBaseDTO BuscarDetalleRubro(int PK_DetalleRubro)
        {
            return new CDetalleRubroL().BuscarDetalleRubro(PK_DetalleRubro);
        }

        public List<CBaseDTO> ObtenerCatalogoRubrosDeduccionPorDetalleDiferencia(int PK_DetalleDiferenciaSalarial)
        {
            return new CDetalleRubroL().ObtenerCatalogoRubrosDeduccionPorDetalleDiferencia(PK_DetalleDiferenciaSalarial);
        }

        /*              Porcentaje Renta                         */

        public CBaseDTO GuardarPorcentajeRenta(CPorcentajeRentaDTO porcRenta)
        {
            return new CPorcentajeRentaL().GuardarPorcentajeRenta(porcRenta);
        }


        public List<CBaseDTO> BuscarPorcentajeRenta(DateTime fecha)
        {
            return new CPorcentajeRentaL().BuscarPorcentajeRenta(fecha);
        }

        /*              Porcentaje Salario Escolar               */

        public CBaseDTO GuardarPorcentajeSalarioEscolar(CPorcentajeSalarioEscolarDTO porcEscolar)
        {
            return new CPorcentajeSalarioEscolarL().GuardarPorcentajeSalarioEscolar(porcEscolar);
        }

        public CBaseDTO BuscarPorcentajeSalarioEscolar(DateTime fecha)
        {
            return new CPorcentajeSalarioEscolarL().BuscarPorcentajeSalarioEscolar(fecha);
        }

        /*              Detalle Deducciones                      */

        public CBaseDTO GuardarDetalleDeducciones(CDetalleDeduccionesDTO item)
        {
            return new CDetalleDeduccionesL().GuardarDetalleDeducciones(item);
        }

        public CBaseDTO BuscarDetalleDeducciones(int PK_DetalleDeducciones)
        {
            return new CDetalleDeduccionesL().BuscarDetalleDeducciones(PK_DetalleDeducciones);
        }

        public List<CBaseDTO> BuscarDetalleDeduccionPorDetalleDiferenciaSalarial(int PK_DetalleDiferenciaSalarial)
        {
            return new CDetalleDeduccionesL().BuscarDetalleDeduccionPorDetalleDiferenciaSalarial(PK_DetalleDiferenciaSalarial);
        }

        /*              Catalogo Tipo Deduccion                  */

        public CBaseDTO GuardarCatalogoTipoDeduccion(CCatalogoTipoDeduccionDTO item)
        {
            return new CCatalogoTipoDeduccionL().GuardarCatalogoTipoDeduccion(item);
        }

        public CBaseDTO BuscarCatalogoTipoDeduccion(int PK_CatalogoTipoDeduccion)
        {
            return new CCatalogoTipoDeduccionL().BuscarCatalogoTipoDeduccion(PK_CatalogoTipoDeduccion);
        }

        public List<List<CBaseDTO>> BuscarRubrosDeducciones(DateTime fecha, int PK_TipoDeduccion)
        {
            return new CCatalogoTipoDeduccionL().BuscarRubrosDeducciones(fecha, PK_TipoDeduccion);

        }

        /*              Rubro Deduccion                          */

        public CBaseDTO GuardarRubroDeduccion(CRubroDeduccionDTO item)
        {
            return new CRubroDeduccionL().GuardarRubroDeduccion(item);
        }

        public CBaseDTO BuscarRubroDeduccion(int PK_RubroDeduccion)
        {
            return new CRubroDeduccionL().BuscarRubroDeduccion(PK_RubroDeduccion);
        }

        public List<CBaseDTO> BuscarDetallesPorCatalogoTipoDeduccion(int PK_CatalogoTipoDeduccion)
        {
            return new CRubroDeduccionL().BuscarDetallesPorCatalogoTipoDeduccion(PK_CatalogoTipoDeduccion);
        }

        /*              Catalogo Rubro Deduccion                 */

        public CBaseDTO GuardarCatalogoRubroDeduccion(CCatalogoRubroDeduccionDTO rubro)
        {
            return new CCatalogoRubroDeduccionL().GuardarCatalogoRubroDeduccion(rubro);
        }

        public CBaseDTO BuscarCatalogoRubroDeduccion(int PK_CatalogoRubroDeduccion)
        {
            return new CCatalogoRubroDeduccionL().BuscarCatalogoRubroDeduccion(PK_CatalogoRubroDeduccion);
        }
        public List<CBaseDTO> ObtenerCatalogoRubrosDeduccion()
        {
            return new CCatalogoRubroDeduccionL().ObtenerCatalogoRubrosDeduccion();
        }

        public CBaseDTO BuscaFuncionarioPorID(string id)
        {
            CFuncionarioL logica = new CFuncionarioL();
            return logica.BuscaFuncionarioPorID(id);
        }
    }
}
