using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIRH.Datos;
using SIRH.DTO;

namespace SIRH.Logica
{
    public class CDiferenciaSalarialL
    {
        #region Variables

        SIRHEntities entidadBase;

        #endregion

        #region Constructor

        public CDiferenciaSalarialL()
        {
            entidadBase = new SIRHEntities();
        }

        #endregion

        #region Metodos

        internal static CDiferenciaSalarialDTO ConvertirDatosDiferenciaSalarialADTO(DiferenciaSalarial item)
        {
            var respuesta = new CDiferenciaSalarialDTO
            {
                IdEntidad = item.PK_DiferenciaSalarial,
                TotalAPagar = item.TotalAPagar,
                Estado = item.IndEstado,
                FecRegistro = item.FecRegistro,
            };

            if (item.Funcionario != null)
            {
                respuesta.Funcionario = CFuncionarioL.ConvertirDatosFuncionarioADTO(item.Funcionario);
            }
            if (item.Puesto != null)
            {
                respuesta.Puesto = CPuestoL.ConvertirCPuestoGeneralDatosaDTO(item.Puesto);
            }
            if (item.Clase != null)
            {
                respuesta.Clase = CClaseL.ConstruirClase(item.Clase);
            }
            if (item.Especialidad != null)
            {
                respuesta.Especialidad = CEspecialidadL.ConstruirEspecialidad(item.Especialidad);
            }
            return respuesta;
        }

        public CBaseDTO GuardarDiferenciaSalarial(CDiferenciaSalarialDTO item)
        {
            CBaseDTO respuesta = new CBaseDTO();
            try
            {
                CDiferenciaSalarialD intermedio = new CDiferenciaSalarialD(entidadBase);
                CFuncionarioD intermedioFuncionario = new CFuncionarioD(entidadBase);
                CPuestoD intermedioPuesto = new CPuestoD(entidadBase);
                CClaseD intermedioClase = new CClaseD(entidadBase);
                CEspecialidadD intermedioEspecialidad = new CEspecialidadD(entidadBase);

                DiferenciaSalarial diferenciaNueva = new DiferenciaSalarial
                {
                    TotalAPagar = item.TotalAPagar,
                    IndEstado = item.Estado,
                    FecRegistro = item.FecRegistro,
                };

                var funcionario = intermedioFuncionario.BuscarFuncionarioBase(item.Funcionario.Cedula);
                if (funcionario.Codigo != -1)
                {
                    diferenciaNueva.FK_Funcionario = ((Funcionario)funcionario.Contenido).PK_Funcionario;
                }
                else
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)funcionario).Contenido;
                    return respuesta;
                }

                var puesto = intermedioPuesto.DescargarPuestoCodigo(item.Puesto.CodPuesto);
                if (puesto.PK_Puesto != -1)
                {
                    diferenciaNueva.FK_Puesto = puesto.PK_Puesto;
                }
                else
                {
                    respuesta = new CErrorDTO { Codigo = -1, Mensaje = "No existe un puesto con el codigo ingresado" };
                    return respuesta;
                }

                var clase = intermedioClase.CargarClasePorID(item.Clase.IdEntidad);
                if (clase.PK_Clase != -1)
                {
                    diferenciaNueva.FK_Clase = clase.PK_Clase;
                }
                else
                {
                    respuesta = new CErrorDTO { Codigo = -1, Mensaje = "No existe una clase con el id ingresado" };
                    return respuesta;
                }

                var especialidad = intermedioEspecialidad.CargarEspecialidadPorID(item.Especialidad.IdEntidad);
                if (especialidad.PK_Especialidad != -1)
                {
                    diferenciaNueva.FK_Especialidad = especialidad.PK_Especialidad;
                }
                else
                {
                    respuesta = new CErrorDTO { Codigo = -1, Mensaje = "No existe una especilidad con el id ingresado" };
                    return respuesta;
                }

                respuesta = intermedio.AgregarDiferenciaSalarial(diferenciaNueva);
                if (((CRespuestaDTO)respuesta).Contenido.GetType() == typeof(CErrorDTO))
                {
                    respuesta = (CErrorDTO)((CRespuestaDTO)respuesta).Contenido;
                    return respuesta;
                }
                respuesta = new CErrorDTO { Codigo = -1, Mensaje = "No se pudo crear la diferencia salarial" };
            }
            catch (Exception error)
            {
                respuesta = new CErrorDTO { MensajeError = error.Message };
            }
            return respuesta;
        }

        public CBaseDTO BuscarDiferenciaSalarial(int PK_DiferenciaSalarial)
        {
            CBaseDTO respuesta;
            try
            {
                CDiferenciaSalarialD intermedio = new CDiferenciaSalarialD(entidadBase);

                var datos = intermedio.BuscarDiferenciaSalarial(PK_DiferenciaSalarial);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    respuesta = ConvertirDatosDiferenciaSalarialADTO(((DiferenciaSalarial)datos.Contenido));
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

        public List<CBaseDTO> BuscarDiferenciasSalarialesPorPuesto(String codPuesto)
        {
            List<CBaseDTO> detalles = new List<CBaseDTO>();
            try
            {
                CDiferenciaSalarialD intermedio = new CDiferenciaSalarialD(entidadBase);

                var datos = intermedio.BuscarDiferenciasSalarialesPorPuesto(codPuesto);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<DiferenciaSalarial>)datos.Contenido))
                    {
                        detalles.Add(ConvertirDatosDiferenciaSalarialADTO(item));
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

        public List<CBaseDTO> FiltrarDiferenciasSalariales(CDiferenciaSalarialDTO filtro, DateTime? inicial, DateTime? final)
        {
            List<CBaseDTO> detalles = new List<CBaseDTO>();
            try
            {
                CDiferenciaSalarialD intermedio = new CDiferenciaSalarialD(entidadBase);

                DiferenciaSalarial diferenciaFiltro = new DiferenciaSalarial
                {
                    IndEstado = filtro.Estado,
                    FecRegistro = filtro.FecRegistro,
                    Puesto = new Puesto { PK_Puesto = filtro.Puesto.IdEntidad },
                    Funcionario = new Funcionario { PK_Funcionario = filtro.Funcionario.IdEntidad },
                    Clase = new Clase { PK_Clase = filtro.Clase.IdEntidad },
                    Especialidad = new Especialidad { PK_Especialidad = filtro.Especialidad.IdEntidad }
                };

                var datos = intermedio.FiltrarDiferenciasSalariales(diferenciaFiltro, inicial, final);
                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<DiferenciaSalarial>)datos.Contenido))
                    {
                        detalles.Add(ConvertirDatosDiferenciaSalarialADTO(item));
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

        public List<CBaseDTO> BuscarDiferenciasSalarialesPorFuncionario(string cedula)
        {
            List<CBaseDTO> detalles = new List<CBaseDTO>();
            try
            {
                CDiferenciaSalarialD intermedio = new CDiferenciaSalarialD(entidadBase);

                var datos = intermedio.BuscarDiferenciasSalarialesPorFuncionario(cedula);

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<DiferenciaSalarial>)datos.Contenido))
                    {
                        detalles.Add(ConvertirDatosDiferenciaSalarialADTO(item));
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

        public List<CBaseDTO> ListarComponentesSalariales()
        {
            List<CBaseDTO> componentes = new List<CBaseDTO>();
            try
            {
                CDiferenciaSalarialD intermedio = new CDiferenciaSalarialD(entidadBase);

                var datos = intermedio.ListarComponentesSalariales();

                if (datos.Contenido.GetType() != typeof(CErrorDTO))
                {
                    foreach (var item in ((List<ComponenteSalarial>)datos.Contenido))
                    {
                        componentes.Add(new CComponenteSalarialDTO
                        {
                            IdEntidad = item.PK_ComponenteSalarial,
                            DesComponenteSalarial = item.DesComponenteSalarial
                        });
                    }
                    return componentes;
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
