﻿using MecanicaBeneteli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        Task<bool> InsereUsuario(Usuario usuario);
        Task<Usuario> ConsultarSenha(string idUsuario);
    }
}
