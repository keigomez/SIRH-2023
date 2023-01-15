using SIRH.Datos;
using SIRH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRH.Logica
{
    public class CPorcentajeSalarioEscolarL
    {
        #region Variables

        SIRHEntities entidadBase;

        #endregion

        #region Constructor

        public CPorcentajeSalarioEscolarL()
        {
            entidadBase = new SIRHEntities();
        }

        #endregion

        #region Metodos

        internal static CPorcentajeSalarioEscolarDTO ConvertirDatosPorcentajeSalarioEscolarADTO(PorcentajeSalarioEscolar item)
        {
            return new CPorcentajeSalarioEscolarDTO
            {
                IdEntidad = item.PK_PorcentajeSalarioEscolar,
                NumResolucion = item.NumResolucion,
                PorcEscolar = item.PorcEscolar,
                FecInicio = item.FecInicio,
                FecFinal = item.FecFinal,
            };
        }

        public CBaseDTO GuardarPorcentajeSalarioEscolar(CPorcentajeSalarioEscolarDTO porcEscolar)
        {
            CBaseDTO respuesta = new CBaseDTO();
            try
            {
                CPorcentajeSalarioEscolarD intermedio = new CPorcentajeSalarioEscolarD(entidadBase);

                PorcentajeSalarioEscolar rubroNuevo = new PorcentajeSalarioEscolar
                {
                    PK_PorcentajeSalarioEscolar = porcEscolar.IdEntidad,
                    NumResolucion = porcEscolar.NumResolucion,
                    PorcEscolar = porcEscolar.PorcEscolar,
                    FecInicio = porcEscolar.FecInicio,
                    FecFinal = porcEscolar.FecFinal,
                };
                respuesta = intermedio.AgregarPorcentajeSalarioEscolar(rubroNuevo);
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

        public CBaseDTO BuscarPorcentajeSalarioEscolar(DateTime fecha)
        {
            CBaseDTO respuesta;
            try
            {
                CPorcentajeSalarioEscolarD intermedio = new CPorcentajeSalarioEscolarD(entidadBase);

                var datos = intermedio.BuscarPorcentajeSalarioEscolar(fecha);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    respuesta = ConvertirDatosPorcentajeSalarioEscolarADTO(((PorcentajeSalarioEscolar)datos.Contenido));
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


        #endregion
    }
}
