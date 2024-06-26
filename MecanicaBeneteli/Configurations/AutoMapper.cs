﻿using AutoMapper;
using MecanicaBeneteli.Business.Models;
using MecanicaBeneteli.ViewModel;

namespace MecanicaBeneteli.Configurations
{
    public class AutoMapper : Profile
    {
        public AutoMapper()        
        {
            CreateMap<UsuarioViewModel, Usuario>().AfterMap((origem, destino) =>
            {
                destino.Nome = origem.Nome;
                destino.CPF = origem.CPF;
                destino.IdUsuario = origem.IdUsuario;
                destino.Senha = origem.Senha;

            });

            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(destino => destino.Nome,
                                map => map.MapFrom(origem => origem.Nome))
                .ForMember(destino => destino.CPF,
                                map => map.MapFrom(origem => origem.CPF))
                .ForMember(destino => destino.IdUsuario,
                                map => map.MapFrom(origem => origem.IdUsuario))
                .ForMember(destino => destino.Senha,
                                map => map.MapFrom(origem => origem.Senha));

            CreateMap<Peca, PecaViewModel>()
                .ForMember(destino => destino.Nome,
                                map => map.MapFrom(origem => origem.Nome))
                .ForMember(destino => destino.Marca,
                                map => map.MapFrom(origem => origem.Marca))
                .ForMember(destino => destino.Modelo,
                                map => map.MapFrom(origem => origem.Modelo))
                .ForMember(destino => destino.Ano,
                                map => map.MapFrom(origem => origem.Ano))
                .ForMember(destino => destino.Preco,
                                map => map.MapFrom(origem => origem.Preco))
                .ForMember(destino => destino.Quantidade,
                                map => map.MapFrom(origem => origem.Quantidade));

            CreateMap<PecaViewModel, Peca>().AfterMap((origem, destino) =>
            {
                destino.Id = origem.Id;
                destino.Nome = origem.Nome;
                destino.Marca = origem.Marca;
                destino.Modelo = origem.Modelo;
                destino.Ano = origem.Ano;
                destino.Preco = origem.Preco;
                destino.Quantidade = origem.Quantidade;

            });



            CreateMap<ManutencaoViewModel, Manutencao>().AfterMap((origem, destino) =>
            {
                destino.NomeUsuario = origem.NomeUsuario;
                destino.CarroUsado = origem.CarroUsado;
                destino.Valor = origem.Valor;
                destino.Placa = origem.Placa;
                if (origem.PecaViewModel1 != null)
                {
                    if (destino.Peca == null)
                    {
                        destino.Peca = new Peca(); // Inicializa o objeto Peca se for null
                    }

                    destino.Peca.Quantidade = origem.PecaViewModel1.Quantidade;
                    destino.Peca.Id = origem.PecaViewModel1.Id;
                }

            });

        }
    }
}
