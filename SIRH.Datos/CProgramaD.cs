using SIRH.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIRH.Datos
{
    public class CProgramaD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion
        
        #region Constructor

        public CProgramaD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion
        
        #region Metodos
        /// <summary>
        /// Guarda el programa
        /// </summary>
        /// <returns>Retorna el programa</returns>
        public int GuardarPrograma(Programa Programa)
        {
            entidadBase.Programa.Add(Programa);
            return Programa.PK_Programa;
        }
       
        /// <summary>
        /// Carga losprogramas de la BD
        /// </summary>
        /// <returns>Retorna programas</returns>
        public Programa CargarProgramaPorID(int idPrograma)
        {
            Programa resultado = new Programa();

            resultado = entidadBase.Programa.Where(R => R.PK_Programa == idPrograma).FirstOrDefault();

            return resultado;
        }

        public CRespuestaDTO ListarProgramas()
        {
            CRespuestaDTO respuesta;

            try
            {
                var datosEntidad = entidadBase.Programa.ToList();

                if (datosEntidad != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = datosEntidad
                    };
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontraron Programas asociados.");
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { Mensaje = error.Message }
                };

                return respuesta;
            }
        }
        #endregion

    }
    
}