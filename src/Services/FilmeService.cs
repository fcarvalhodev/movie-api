﻿using AutoMapper;
using FilmeApi2.Data;
using FilmeApi2.Dtos;
using FilmeApi2.Helpers;
using System;

namespace FilmeApi2.Services
{
    /// <summary>
    /// Classe de serviço de filme deve ter regras de negócio.
    /// Em alguns casos usamos os repotirio para deixar a regra de negócio no serviço e a regra de banco no repositório.
    /// No caso abaixo eu não criei para não complicar o exemplo.
    /// Mas, caso houvesse a camada de repositório, todo o processo de "salvar, alterar, excluir, etc" seria feito nessa camada, mas a regra continuaria nessa classe.
    /// Esse padrão é chamado de "Repository Pattern".
    /// </summary>
    public class FilmeService : IFilmeService
    {
        private IMapper _mapper;
        
        //opcional, pode usar repositório para deixar a regra de negócio no serviço e a regra de banco no repositório
        private IFilmeContext _context; 
        
        public FilmeService(IMapper mapper, IFilmeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Método para criar um filme.
        /// Aqui, eu acrescentei algo que faltou no seu projeto que é o try/catch.
        /// O try/catch é importante para que o sistema não quebre caso ocorra algum erro.
        /// Anteriormente em caso de erro 500 (internal server), o sistema quebrava.
        /// Agora, com a nossa mensagem customizada da ApiResponse, nós podemos retornar o erro para o usuário de forma amigável e legível.
        /// </summary>
        /// <param name="createFilmeDto"></param>
        /// <returns></returns>
        public ApiResponse<CreateFilmeDto> CreateFilme(CreateFilmeDto createFilmeDto)
        {
            try
            {
                Filme filme = _mapper.Map<Filme>(createFilmeDto);
                _context.Filmes.Add(filme);
                _context.SaveChanges();
                return ApiResponse<CreateFilmeDto>.Success(createFilmeDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<CreateFilmeDto>.Error(ex.Message);
            }
        }
    }
}
