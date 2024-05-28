using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ranqueamento.API.Configuracao;
using Ranqueamento.API.Controllers;
using Ranqueamento.API.DataBase;
using Ranqueamento.API.Dtos;
using Ranqueamento.API.Helpers;
using Ranqueamento.API.Interfaces;
using Ranqueamento.API.Model;
using Ranqueamento.API.Services;
using Ranqueamento.UnitTests.Fixtures;
using Xunit;

namespace Ranqueamento.UnitTests;
public class RanqueamentoTest
{
    private readonly Configuracoes _configuracoes;
    private readonly VerificaPessoaMenoridade _verificaPessoaMenoridade;
    private readonly CalcularPontosDependente _calcularPontoDependente;
    private readonly CalcularPontosRenda _calcularPontoRenda;
    private readonly ObterDependentesPontuantes _obterDependentesPontuantes;
    private readonly CalcularPontos _calcularPontos;
    private readonly RanqueadorFamilias _ranqueadorFamilias;
    private readonly RanqueadorController _ranqueadorController;
    private readonly Mock<IFamiliaRepositorio> _familiaRepositorioMock;
    private readonly Mock<IRanqueadorFamilias> _ranqueadorFamiliasMock;    
    
    public RanqueamentoTest()
    {
        _configuracoes = new Configuracoes();
        _verificaPessoaMenoridade = new VerificaPessoaMenoridade();
        _obterDependentesPontuantes = new ObterDependentesPontuantes(_verificaPessoaMenoridade);
        _calcularPontoDependente = new CalcularPontosDependente(_obterDependentesPontuantes, _configuracoes);
        _calcularPontoRenda = new CalcularPontosRenda(_configuracoes);

        _calcularPontoDependente =
            new CalcularPontosDependente(
                _obterDependentesPontuantes,
                _configuracoes);        

        _calcularPontos = new CalcularPontos(_calcularPontoDependente, _calcularPontoRenda);
        _ranqueadorFamilias = new RanqueadorFamilias(_calcularPontos);

        _familiaRepositorioMock = new Mock<IFamiliaRepositorio>();
        _ranqueadorFamiliasMock = new Mock<IRanqueadorFamilias>();

        _ranqueadorController = 
            new RanqueadorController(_familiaRepositorioMock.Object, _ranqueadorFamiliasMock.Object);
    }

    [Fact]
    public void DeveCriarUmaPessoaComMaioridade()
    {
        //Arrange
        var pessoa = PessoaBuilder.Novo()
                                   .ComNome(false)
                                   .ComMaioridade()
                                   .Obter();

        //Act
        var resultado = _verificaPessoaMenoridade.Verificar(pessoa);
        
        //Assert
        resultado.Should().Be(false);
    }

    [Fact]
    public void DeveCriarUmaPessoaComMenoridade()
    {
        //Arrange
        var pessoa = PessoaBuilder.Novo()
                                   .ComNome(false)
                                   .ComMenoridade()
                                   .Obter();

        //Act
        var resultado = _verificaPessoaMenoridade.Verificar(pessoa);

        //Assert
        resultado.Should().Be(true);
    }

    [Fact]
    public void DeveValidarPontuacaoComNenhumDependente()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Zero;
        var dependentes = DependentesBuilder.Novo()
                                            .ComMenoridade(Constantes.Zero)
                                            .Obter();

        //Act
        var resultado = _calcularPontoDependente.Calcular(dependentes);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarPontuacaoComUmDependente()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Dois;
        var dependentes = DependentesBuilder.Novo()
                                            .ComMenoridade(Constantes.Um)
                                            .Obter();

        //Act
        var resultado = _calcularPontoDependente.Calcular(dependentes);

        //Assert
        resultado.Should().Be(valorEsperado);
    }


    [Fact]
    public void DeveValidarPontuacaoComDoisDependentes()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Dois;
        var dependentes = DependentesBuilder.Novo()
                                            .ComMenoridade(Constantes.Dois)
                                            .Obter();

        //Act
        var resultado = _calcularPontoDependente.Calcular(dependentes);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarPontuacaoComTresDependentes()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Tres;
        var dependentes = DependentesBuilder.Novo()
                                            .ComMenoridade(Constantes.Tres)
                                            .Obter();

        //Act
        var resultado = _calcularPontoDependente.Calcular(dependentes);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarPontuacaoComQuatroDependentes()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Tres;
        var dependentes = DependentesBuilder.Novo()
                                            .ComMenoridade(Constantes.Quatro)
                                            .Obter();

        //Act
        var resultado = _calcularPontoDependente.Calcular(dependentes);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarPontuacaoRendaComNenhumaRenda()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Cinco;
        var rendaTotal = Constantes.ZeroDouble;

        //Act
        var resultado = _calcularPontoRenda.Calcular(rendaTotal);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarPontuacaoRendaComNovecentos()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Cinco;
        var rendaTotal = Constantes.NovecentosDouble;

        //Act
        var resultado = _calcularPontoRenda.Calcular(rendaTotal);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarPontuacaoRendaComNovecentosUm()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Tres;
        var rendaTotal = Constantes.NovecentosUmDouble;

        //Act
        var resultado = _calcularPontoRenda.Calcular(rendaTotal);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarPontuacaoRendaComMilQuinhentos()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Tres;
        var rendaTotal = Constantes.MilQuinhentosDouble;

        //Act
        var resultado = _calcularPontoRenda.Calcular(rendaTotal);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarPontuacaoRendaComMilQuinhentosUm()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Zero;
        var rendaTotal = Constantes.MilQuinhentosUmDouble;

        //Act
        var resultado = _calcularPontoRenda.Calcular(rendaTotal);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarCalcularPontosOito()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Oito;
        var familia = FamiliaBuilder.Novo()
                                    .ComNome()
                                    .ComCpf()
                                    .ComConjuge()
                                    .ComDependenteMenoridade(3)
                                    .ComRendaTotal(Constantes.NovecentosDouble)                                    
                                    .Obter();

        
        //Act        
        var resultado = _calcularPontos.Calcular(familia);
        
        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarCalcularPontosSete()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Sete;
        var familia = FamiliaBuilder.Novo()
                                    .ComNome()
                                    .ComCpf()
                                    .ComConjuge()
                                    .ComDependenteMenoridade(2)
                                    .ComRendaTotal(Constantes.NovecentosDouble)
                                    .Obter();


        //Act        
        var resultado = _calcularPontos.Calcular(familia);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarCalcularPontosSeis()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Seis;
        var familia = FamiliaBuilder.Novo()
                                    .ComNome()
                                    .ComCpf()
                                    .ComConjuge()
                                    .ComDependenteMenoridade(3)
                                    .ComRendaTotal(Constantes.NovecentosUmDouble)
                                    .Obter();


        //Act        
        var resultado = _calcularPontos.Calcular(familia);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarCalcularPontosCinco()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Cinco;
        var familia = FamiliaBuilder.Novo()
                                    .ComNome()
                                    .ComCpf()
                                    .ComConjuge()
                                    .ComDependenteMenoridade(2)
                                    .ComRendaTotal(Constantes.NovecentosUmDouble)
                                    .Obter();


        //Act        
        var resultado = _calcularPontos.Calcular(familia);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarCalcularPontosTres()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Tres;
        var familia = FamiliaBuilder.Novo()
                                    .ComNome()
                                    .ComCpf()
                                    .ComConjuge()                                    
                                    .ComRendaTotal(Constantes.NovecentosUmDouble)
                                    .Obter();


        //Act        
        var resultado = _calcularPontos.Calcular(familia);

        //Assert
        resultado.Should().Be(valorEsperado);
    }


    [Fact]
    public void DeveValidarCalcularPontosDois()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Dois;
        var familia = FamiliaBuilder.Novo()
                                    .ComNome()
                                    .ComCpf()
                                    .ComConjuge()
                                    .ComDependenteMenoridade(2)
                                    .ComRendaTotal(Constantes.MilQuinhentosUmDouble)
                                    .Obter();


        //Act        
        var resultado = _calcularPontos.Calcular(familia);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarCalcularPontosZero()
    {
        //Arrange                                                                
        var valorEsperado = Constantes.Zero;
        var familia = FamiliaBuilder.Novo()
                                    .ComNome()
                                    .ComCpf()
                                    .ComConjuge()                                    
                                    .ComRendaTotal(Constantes.MilQuinhentosUmDouble)
                                    .Obter();


        //Act        
        var resultado = _calcularPontos.Calcular(familia);

        //Assert
        resultado.Should().Be(valorEsperado);
    }

    [Fact]
    public void DeveValidarRanqueador()
    {
        //Arrange
        var random = new Random();
        var amostragem = random.Next(Constantes.Cinco, Constantes.Oito);        
        var familias = new List<Familia>();

        while(amostragem > Constantes.Zero)
        {
            var menoridades = random.Next(Constantes.Zero, Constantes.Quatro);
            var maioridades = random.Next(Constantes.Zero, Constantes.Quatro);
            var renda = (double)random.Next(Constantes.Zero, Constantes.MilQuinhentosUm);

            var familia = FamiliaBuilder.Novo()
                                        .Completa(renda, menoridades, maioridades)
                                        .Obter();
            familias.Add(familia);
            amostragem--;
        }

        //Act        
        var resultado = _ranqueadorFamilias.Ranquear(familias);

        //Assert
        var consisteste = true;
        var primeiraFamilia = resultado.First();
        var maiorPontuacao = primeiraFamilia.Pontuacao?? Constantes.Zero;

        foreach(var item in resultado)
        {
             if(maiorPontuacao < item.Pontuacao)
            {
                consisteste = false;
                break;
            }
        }

        consisteste.Should().Be(true);
    }

    [Fact]
    public async void DeveValidarRanqueadorControllerObterFamiliasRanqueadas()
    {           
        // Arrange
        _familiaRepositorioMock.Setup(repo => repo.ObterTodosAsync())
                                .ReturnsAsync(new List<Familia>());

        _ranqueadorFamiliasMock.Setup(ranqueador => ranqueador.Ranquear(It.IsAny<List<Familia>>()))
                                .Returns(new List<FamiliaDto>());
        // Act
        var result = await _ranqueadorController.ObterListaRanqueadaFamilias();
        var okResult = Assert.IsType<OkObjectResult>(result);

        // Assert

        _familiaRepositorioMock.Verify(repositorio => repositorio.ObterTodosAsync(), Times.Once);
        _ranqueadorFamiliasMock.Verify(ranqueador => ranqueador.Ranquear(It.IsAny<List<Familia>>()), Times.Once);
        okResult.Value.Should().NotBe(null);
    }
}
