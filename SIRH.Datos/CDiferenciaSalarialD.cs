using System;
using System.Collections.Generic;
using System.Linq;
using SIRH.DTO;

namespace SIRH.Datos
{
    public class CDiferenciaSalarialD
    {
        #region Variables

        private SIRHEntities entidadBase = new SIRHEntities();

        #endregion

        #region Constructor

        public CDiferenciaSalarialD(SIRHEntities entidadGlobal)
        {
            entidadBase = entidadGlobal;
        }

        #endregion

        #region Metodos

        public CRespuestaDTO AgregarDiferenciaSalarial(DiferenciaSalarial diferenciaSalarial)
        {
            CRespuestaDTO respuesta;
            try
            {
                entidadBase.DiferenciaSalarial.Add(diferenciaSalarial);
                entidadBase.SaveChanges();
                respuesta = new CRespuestaDTO
                {
                    Codigo = 1,
                    Contenido = diferenciaSalarial.PK_DiferenciaSalarial
                };
                return respuesta;
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
                return respuesta;
            }
        }

        public CRespuestaDTO BuscarDiferenciasSalarialesPorPuesto(String CodPuesto)
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.DiferenciaSalarial
                    .Include("Funcionario")
                    .Include("Puesto")
                    .Include("Clase")
                    .Include("Especialidad")
                    .Where(D => D.Puesto.CodPuesto == CodPuesto)
                    .OrderBy(Q => Q.FecRegistro)
                    .ToList();

                if (lista != null && lista.Count() > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = lista
                    };
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontraron diferencias salariales para el código de puesto consultado");
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
                return respuesta;
            }
        }

        public CRespuestaDTO BuscarDiferenciaSalarial(int PK_DiferenciaSalarial)
        {
            CRespuestaDTO respuesta;
            try
            {
                var estudioP = entidadBase.DiferenciaSalarial
                        .Include("DetalleDiferenciaSalarial")
                        .Where(P => P.PK_DiferenciaSalarial == PK_DiferenciaSalarial)
                        .FirstOrDefault();
                if (estudioP != null)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = estudioP
                    };
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontró la diferencia salarial con el id consultado");
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
                return respuesta;
            }
        }

        public CRespuestaDTO FiltrarDiferenciasSalariales(DiferenciaSalarial filtro, DateTime? inicial, DateTime? final)
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.DiferenciaSalarial
                        .Where(Q => filtro.Puesto == null || Q.Puesto.CodPuesto.Contains(filtro.Puesto.CodPuesto))
                        .Where(Q => filtro.Funcionario == null || Q.Funcionario.IdCedulaFuncionario.Contains(filtro.Funcionario.IdCedulaFuncionario))
                        .Where(Q => filtro.Clase == null || Q.Clase.DesClase.Contains(filtro.Clase.DesClase))
                        .Where(Q => filtro.Especialidad == null || Q.Especialidad.DesEspecialidad.Contains(filtro.Especialidad.DesEspecialidad))
                        .Where(Q => filtro.IndEstado == null || Q.IndEstado == filtro.IndEstado)
                        .Where(Q => inicial == null || Q.FecRegistro >= inicial)
                        .Where(Q => final == null || Q.FecRegistro <= final)
                        .OrderByDescending(Q => Q.FecRegistro)
                        .ToList();

                if (lista != null && lista.Count() > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = lista
                    };
                }
                else
                {
                    throw new Exception("No se encontraron diferencias salariales");
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
            }
            return respuesta;
        }

        public CRespuestaDTO BuscarDiferenciasSalarialesPorFuncionario(string cedula)
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.DiferenciaSalarial
                    .Include("Funcionario")
                    .Include("Puesto")
                    .Include("Clase")
                    .Include("Especialidad")
                    .Where(D => D.Funcionario.IdCedulaFuncionario == cedula)
                    .OrderBy(Q => Q.FecRegistro)
                    .ToList();

                if (lista != null && lista.Count() > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = lista
                    };
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontraron diferencias salariales para el funcionario con la cedula ingresada");
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
                return respuesta;
            }
        }

        public CRespuestaDTO ListarComponentesSalariales()
        {
            CRespuestaDTO respuesta;
            try
            {
                var lista = entidadBase.ComponenteSalarial.ToList();
                if (lista != null && lista.Count() > 0)
                {
                    respuesta = new CRespuestaDTO
                    {
                        Codigo = 1,
                        Contenido = lista
                    };
                    return respuesta;
                }
                else
                {
                    throw new Exception("No se encontraron componentes salariales");
                }
            }
            catch (Exception error)
            {
                respuesta = new CRespuestaDTO
                {
                    Codigo = -1,
                    Contenido = new CErrorDTO { MensajeError = error.Message }
                };
                return respuesta;
            }
        }

        #endregion
    }
}