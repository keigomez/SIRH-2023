using SIRH.Datos;
using SIRH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRH.Logica
{
    public class CObjetoGastoL
    {
        #region Variables

        SIRHEntities contexto;

        #endregion

        #region Constructor

        public CObjetoGastoL()
        {
            contexto = new SIRHEntities();
        }

        #endregion

        #region Métodos

        public List<CBaseDTO> ListarObjetoGasto()
        {
            List<CBaseDTO> respuesta = new List<CBaseDTO>();

            CObjetoGastoD intermedio = new CObjetoGastoD(contexto);

            var objetoGasto = intermedio.ListarObjetosGasto();

            if (objetoGasto.Codigo != -1)
            {
                foreach (var item in (List<ObjetoGasto>)objetoGasto.Contenido)
                {
                    respuesta.Add(new CObjetoGastoDTO
                    {
                        IdEntidad = item.PK_ObjetoGasto,
                        DesObjGasto = item.DesObjetoGasto
                    });
                }
            }
            else
            {
                respuesta.Add((CErrorDTO)objetoGasto.Contenido);
            }

            return respuesta;
        }

        internal static CObjetoGastoDTO ConvertirObjetoGastoADto(ObjetoGasto item)
        {
            return new CObjetoGastoDTO
            {
                IdEntidad = item.PK_ObjetoGasto,
                DesObjGasto = item.DesObjetoGasto
            };
        }

        #endregion
    }
}

