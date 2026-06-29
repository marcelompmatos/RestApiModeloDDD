using BCrypt.Net;
using RestApiModeloDDD.Domain.Entities;
using RestApiModeloDDD.Domain.Exceptions;
using RestApiModeloDDD.Domain.Interfaces.Repositories;
using RestApiModeloDDD.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

public class ServiceAuth : IServiceAuth
{
    private readonly IRepositoryUsuario _repositoryUsuario;
    private readonly IRepositoryRefreshToken _repositoryRefreshToken;
    private readonly IJwtService _jwtService;

    public ServiceAuth(
        IRepositoryUsuario repositoryUsuario,
        IRepositoryRefreshToken repositoryRefreshToken,
        IJwtService jwtService)
    {
        _repositoryUsuario = repositoryUsuario;
        _repositoryRefreshToken = repositoryRefreshToken;
        _jwtService = jwtService;
    }

    public async Task<Usuario> ValidarUsuario(string email, string senha)
    {
        var usuario = await _repositoryUsuario.ObterPorEmail(email);

        if (usuario == null)
            throw new Exception("Usuário não encontrado.");

        if (!BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            throw new UnauthorizedException("Senha inválida.");

        return usuario;
    }

    public async Task<Usuario> ValidarRefreshToken(string token)
    {
        var refreshToken = await _repositoryRefreshToken.ObterPorToken(token);

        if (refreshToken == null)
            return null;

        if (refreshToken.ExpiraEm < DateTime.UtcNow)
            return null;

        if (refreshToken.Revogado)
            return null;

        return await _repositoryUsuario.ObterPorId(refreshToken.UsuarioId);
    }

    public async Task<string> GerarRefreshToken( Usuario usuario)
    {
        var refreshToken =
            _jwtService.GenerateRefreshToken();

        await _repositoryRefreshToken.Adicionar(
            new RefreshToken
            {
                UsuarioId = usuario.Id,
                TokenHash = refreshToken,
                CriadoEm = DateTime.UtcNow,
                ExpiraEm = DateTime.UtcNow.AddDays(7),
                Revogado = false
            });

        return refreshToken;
    }

    public async Task<Usuario> RenovarRefreshToken(string refreshToken)
    {
        var tokenDb =
            await _repositoryRefreshToken
                .ObterPorToken(refreshToken);

        if (tokenDb == null)
            throw new UnauthorizedException(
                "Refresh token inválido.");

        if (tokenDb.Revogado)
            throw new UnauthorizedException(
                "Refresh token revogado.");

        if (tokenDb.ExpiraEm < DateTime.UtcNow)
            throw new UnauthorizedException(
                "Refresh token expirado.");

        tokenDb.Revogado = true;

        await _repositoryRefreshToken
            .Atualizar(tokenDb);

        return await _repositoryUsuario
            .ObterPorId(tokenDb.UsuarioId);
    }
}