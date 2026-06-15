using BCrypt.Net;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

public class ServiceAuth : IServiceAuth
{
    private readonly IRepositoryUsuario _repositoryUsuario;

    public ServiceAuth(
        IRepositoryUsuario repositoryUsuario)
    {
        _repositoryUsuario = repositoryUsuario;
    }

    public async Task<Usuario> ValidarUsuario(
        string email,
        string senha)
    {
        var usuario =
            await _repositoryUsuario
                .ObterPorEmail(email);

        if (usuario == null)
            throw new Exception("Usuário não encontrado.");

        //var senhas = "123456";
        //var hash = BCrypt.Net.BCrypt.HashPassword(senhas);


        if (!BCrypt.Net.BCrypt.Verify(
                senha,
                usuario.SenhaHash))
        {
            throw new Exception("Senha inválida.");
        }

        return usuario;
    }
}