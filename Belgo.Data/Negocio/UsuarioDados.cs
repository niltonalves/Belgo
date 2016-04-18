using Belgo.Dados.Entidade;
using Belgo.Dados.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Belgo.Dados.Negocio
{
    public class UsuarioDados
    {
        Entities db = new Entities();

        bool disposed = false;

        public List<Usuario> Listar(bool bloqueado)
        {
            try
            {
                var retorno = db.SCA_USUARIO
                    .Where(u => u.IND_EXCLUIDO == (bloqueado ? "S" : "N"))
                .Select(c => new Usuario()
                {
                    ID = c.COD_USUARIO,
                    Nome = c.NOM_USUARIO,
                    Email = c.DSC_EMAIL,
                    Ativo = (c.IND_EXCLUIDO == "S" ? true : false)
                }).OrderBy(c => c.Nome).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Usuario Consultar(long id)
        {
            try
            {
                var retorno = db.SCA_USUARIO.Where(u => u.COD_USUARIO == id)
                    .Select(c => new Usuario()
                    {
                        ID = c.COD_USUARIO,
                        Nome = c.NOM_USUARIO,
                        Email = c.DSC_EMAIL,
                        Ativo = (c.IND_EXCLUIDO == "S" ? true : false),
                        Senha = c.PSW_SENHA
                    }).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Usuario Consultar(string email)
        {
            try
            {
                var retorno = db.SCA_USUARIO.Where(u => u.DSC_EMAIL == email)
                    .Select(c => new Usuario()
                    {
                        ID = c.COD_USUARIO,
                        Nome = c.NOM_USUARIO,
                        Email = c.DSC_EMAIL,
                        Ativo = (c.IND_EXCLUIDO == "S" ? true : false)
                    }).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long Cadastrar(Usuario usuario)
        {
            try
            {
                var cadastro = new SCA_USUARIO()
                {
                    DSC_EMAIL = usuario.Email,
                    IND_EXCLUIDO = (usuario.Ativo ? "S" : "N"),
                    PSW_SENHA = usuario.Senha,
                    NOM_USUARIO = usuario.Nome

                };

                db.Entry(cadastro).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();

                return cadastro.COD_USUARIO;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void Atualizar(Usuario usuario)
        {
            try
            {
                var cadastro = new SCA_USUARIO()
                {
                    COD_USUARIO = usuario.ID,
                    DSC_EMAIL = usuario.Email,
                    IND_EXCLUIDO = (usuario.Ativo ? "S" : "N"),
                    PSW_SENHA = usuario.Senha,
                    NOM_USUARIO = usuario.Nome
                
                };

                db.Entry(cadastro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public Usuario Autenticar(Usuario usuario)
        {
            try
            {
                var dados = db.SCA_USUARIO.Where(u => u.DSC_EMAIL == usuario.Email && u.PSW_SENHA==usuario.Senha && u.IND_EXCLUIDO!="S")
                    .Select(c=> new Usuario() { ID = c.COD_USUARIO, Email = c.DSC_EMAIL, Nome= c.NOM_USUARIO }).FirstOrDefault();
                return dados;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public void Deletar(long id)
        {
            try
            {
                var usuario = Consultar(id);
               
                var cadastro = new SCA_USUARIO()
                {
                    COD_USUARIO = usuario.ID,
                    DSC_EMAIL = usuario.Email,
                    IND_EXCLUIDO = "S",
                    PSW_SENHA = usuario.Senha,
                    NOM_USUARIO = usuario.Nome,
                };


                db.Entry(cadastro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #region implementação de dispose
        ~UsuarioDados()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
        #endregion

    }
}
