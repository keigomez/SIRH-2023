using SIRH.Datos;
using SIRH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRH.Logica
{
    public class CProgramaL
    {
        #region Variables

        SIRHEntities contexto;

        #endregion

        #region Constructor

        public CProgramaL()
        {
            contexto = new SIRHEntities();
        }

        #endregion

        #region Métodos

        public List<CBaseDTO> ListarProgramas()
        {
            List<CBaseDTO> respuesta = new List<CBaseDTO>();

            CProgramaD intermedio = new CProgramaD(contexto);

            var programa = intermedio.ListarProgramas();

            if (programa.Codigo != -1)
            {
                foreach (var item in (List<Programa>)programa.Contenido)
                {
                    respuesta.Add(new CProgramaDTO
                    {
                        IdEntidad = item.PK_Programa,
                        DesPrograma = item.DesPrograma
                    });
                }
            }
            else
            {
                respuesta.Add((CErrorDTO)programa.Contenido);
            }

            return respuesta;
        }

        internal static CProgramaDTO ConvertirProgramaADto(Programa item)
        {
            return new CProgramaDTO
            {
                IdEntidad = item.PK_Programa,
                DesPrograma = item.DesPrograma
            };
        }

        #endregion
    }
}


