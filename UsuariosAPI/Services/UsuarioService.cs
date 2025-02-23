using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services;

public class UsuarioService
{
    private IMapper _mapper;
    private UserManager<Usuario> _userManager;
    private SignInManager<Usuario> _signInManager;
    private TokenService _tokenService;


    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager,SignInManager<Usuario> sgInM, TokenService tokenService)
    {
        _signInManager = sgInM;
        _mapper = mapper;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task Cadastra(CreateUsuarioDto dto)
    {
        var user = _mapper.Map<CreateUsuarioDto, Usuario>(dto);
        IdentityResult resultado = await _userManager.CreateAsync(user, dto.Password);

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Falha ao cadastrar o Usuario");
        }
    }

    public async Task<string> Login(LoginUsuarioDto dto)
    {
        var resultado = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Usuario não autenticado");
        }
        var user = await _userManager.FindByNameAsync(dto.UserName);
       
        var token = _tokenService.GenerateToken(user!);
        return token;
    }

    
}

