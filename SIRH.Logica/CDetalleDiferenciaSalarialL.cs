using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIRH.Datos;
using SIRH.DTO;

namespace SIRH.Logica
{
    public class CDetalleDiferenciaSalarialL
    {
        #region Variables

        SIRHEntities entidadBase;

        #endregion

        #region Constructor

        public CDetalleDiferenciaSalarialL()
        {
            entidadBase = new SIRHEntities();
        }

        #endregion

        #region Metodos

        internal static CDetalleDiferenciaSalarialDTO ConvertirDatosDetalleDiferenciaSalarialADTO(DetalleDiferenciaSalarial item)
        {
            var respuesta = new CDetalleDiferenciaSalarialDTO
            {
                IdEntidad = item.PK_DetalleDiferenciaSalarial,
                FecInicio = item.FecInicio,
                FecFinal = item.FecFinal,
                TotalDiferencia = item.TotalDiferencia,
                TotalSalarioEscolar = item.TotalSalarioEscolar,
                TotalDeduccionesObrero = item.TotalDeduccionesObrero,
                TotalDeduccionesPatronal = item.TotalDeduccionesPatronal,
                TotalRenta = item.TotalRenta,
                TotalAguinaldo = item.TotalAguinaldo
            };
            if (item.DiferenciaSalarial != null)
            {
                respuesta.DiferenciaSalarial = CDiferenciaSalarialL.ConvertirDatosDiferenciaSalarialADTO(item.DiferenciaSalarial);
            }
            if (item.PorcentajeRenta != null)
            {
                respuesta.PorcentajeRenta = CPorcentajeRentaL.ConvertirDatosPorcentajeRentaADTO(item.PorcentajeRenta);
            }
            if (item.PorcentajeSalarioEscolar != null)
            {
                respuesta.PorcentajeSalarioEscolar = CPorcentajeSalarioEscolarL.ConvertirDatosPorcentajeSalarioEscolarADTO(item.PorcentajeSalarioEscolar);
            }
            return respuesta;
        }

        public CBaseDTO GuardarDetalleDiferenciaSalarial(CDetalleDiferenciaSalarialDTO item)
        {
            CBaseDTO respuesta = new CBaseDTO();
            try
            {
                CDetalleDiferenciaSalarialD intermedio = new CDetalleDiferenciaSalarialD(entidadBase);
                CDetalleDiferenciaSalarialD intermedioDiferencia = new CDetalleDiferenciaSalarialD(entidadBase);
                CPorcentajeSalarioEscolarD intermedioSalarioEscolar = new CPorcentajeSalarioEscolarD(entidadBase);
                CPorcentajeRentaD intermedioRenta = new CPorcentajeRentaD(entidadBase);

                DetalleDiferenciaSalarial detalleNuevo = new DetalleDiferenciaSalarial
                {
                    FecInicio = item.FecInicio,
                    FecFinal = item.FecFinal,
                    TotalDiferencia = item.TotalDiferencia,
                    TotalSalarioEscolar = item.TotalSalarioEscolar,
                    TotalDeduccionesObrero = item.TotalDeduccionesObrero,
                    TotalDeduccionesPatronal = item.TotalDeduccionesPatronal,
                    TotalRenta = item.TotalRenta,
                    TotalAguinaldo = item.TotalAguinaldo
                };

                var diferenciaSalarial = intermedioDiferencia.BuscarDetalleDiferenciaSalarial(detalleNuevo.FK_DiferenciaSalarial ?? 0);
                if (diferenciaSalarial.Codigo != -1)
                {
                    detalleNuevo.FK_DiferenciaSalarial = ((DiferenciaSalarial)diferenciaSalarial.Contenido).PK_DiferenciaSalarial;
                }
                else
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)diferenciaSalarial).Contenido;
                    return respuesta;
                }

                var salarioEscolar = intermedioSalarioEscolar.ObtenerPorcentajeSalarioEscolar(detalleNuevo.FK_PorcentajeSalarioEscolar ?? 0);
                if (salarioEscolar.Codigo != -1)
                {
                    detalleNuevo.FK_PorcentajeSalarioEscolar = ((PorcentajeSalarioEscolar)salarioEscolar.Contenido).PK_PorcentajeSalarioEscolar;
                }
                else
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)salarioEscolar).Contenido;
                    return respuesta;
                }

                var porcRenta = intermedioRenta.ObtenerPorcentajeRenta(detalleNuevo.FK_PorcentajeRenta ?? 0);
                if (porcRenta.Codigo != -1)
                {
                    detalleNuevo.FK_PorcentajeRenta = ((PorcentajeRenta)porcRenta.Contenido).PK_PorcentajeRenta;
                }
                else
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)porcRenta).Contenido;
                    return respuesta;
                }

                respuesta = intermedio.AgregarDetalleDiferenciaSalarial(detalleNuevo);
                if (((CRespuestaDTO)respuesta).Contenido.GetType() == typeof(CErrorDTO))
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)respuesta).Contenido;
                    return respuesta;
                }

            }
            catch (Exception error)
            {
                respuesta = new CErrorDTO { MensajeError = error.Message };
            }
            return respuesta;
        }

        public CBaseDTO BuscarDetalleDiferenciaSalarial(int PK_DetalleDiferenciaSalarial)
        {
            CBaseDTO respuesta;
            try
            {
                CDetalleDiferenciaSalarialD intermedio = new CDetalleDiferenciaSalarialD(entidadBase);

                var datos = intermedio.BuscarDetalleDiferenciaSalarial(PK_DetalleDiferenciaSalarial);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    respuesta = ConvertirDatosDetalleDiferenciaSalarialADTO(((DetalleDiferenciaSalarial)datos.Contenido));
                    return respuesta;
                }
                else
                {
                    throw new Exception(((CErrorDTO)datos.Contenido).MensajeError);
                }
            }
            catch (Exception error)
            {
                respuesta = new CErrorDTO { MensajeError = error.Message };
                return respuesta;
            }
        }

        public List<CBaseDTO> BuscarDetallesPorDiferenciaSalarial(int PK_DiferenciaSalarial)
        {
            List<CBaseDTO> detalles = new List<CBaseDTO>();
            try
            {
                CDetalleDiferenciaSalarialD intermedio = new CDetalleDiferenciaSalarialD(entidadBase);

                var datos = intermedio.BuscarDetallesPorDiferenciaSalariales(PK_DiferenciaSalarial);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<DetalleDiferenciaSalarial>)datos.Contenido))
                    {
                        detalles.Add(ConvertirDatosDetalleDiferenciaSalarialADTO(item));
                    }
                    return detalles;
                }
                else
                {
                    throw new Exception(((CErrorDTO)datos.Contenido).MensajeError);
                }
            }
            catch (Exception error)
            {
                return new List<CBaseDTO> { new CErrorDTO { MensajeError = error.Message } };
            }
        }


        #endregion
    }
}