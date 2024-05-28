using Microsoft.EntityFrameworkCore.Migrations;

public static class AdicaoDeFamiliasParaTesteSeed
{
    public static void Seed(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
                    INSERT INTO dbo.Familia (Nome, Cpf, Conjuge, RendaTotal)
                    VALUES
                        ('Família Silva', '11111111111', 'Cônjuge Silva', ROUND(RAND() * (2000 - 800) + 800, 2)),
                        ('Família Santos', '22222222222', 'Cônjuge Santos', ROUND(RAND() * (2000 - 800) + 800, 2)),
                        ('Família Oliveira', '33333333333', 'Cônjuge Oliveira', ROUND(RAND() * (2000 - 800) + 800, 2)),
                        ('Família Souza', '44444444444', 'Cônjuge Souza', ROUND(RAND() * (2000 - 800) + 800, 2)),
                        ('Família Pereira', '55555555555', 'Cônjuge Pereira', ROUND(RAND() * (2000 - 800) + 800, 2));

                    -- Inserção de dados na tabela Pessoa
                    DECLARE @FamiliaId INT;

                    -- Inserção de pessoas para cada família
                    SET @FamiliaId = (SELECT MAX(Id) FROM dbo.Familia) - 4; -- Obtém o ID da primeira família inserida

                    WHILE @FamiliaId <= (SELECT MAX(Id) FROM dbo.Familia) -- Para cada família
                    BEGIN
                        DECLARE @NumDependentes INT;
                        SET @NumDependentes = CAST(RAND() * 5 AS INT); -- Gera um número aleatório de dependentes de 0 a 4
    
                        -- Inserção de dependentes
                        DECLARE @DependenteId INT = 1;
                        WHILE @DependenteId <= @NumDependentes
                        BEGIN
                            DECLARE @NomeDependente VARCHAR(50);
                            DECLARE @IdadeDependente INT;
        
                            -- Gera um nome de dependente aleatório
                            SET @NomeDependente = CASE 
                                                    WHEN @DependenteId = 1 THEN 'Filho Mais Velho ' + (SELECT Conjuge FROM dbo.Familia WHERE Id = @FamiliaId)
                                                    WHEN @DependenteId = 2 THEN 'Filho Mais Novo ' + (SELECT Conjuge FROM dbo.Familia WHERE Id = @FamiliaId)
                                                    WHEN @DependenteId = 3 THEN 'Filha ' + (SELECT Conjuge FROM dbo.Familia WHERE Id = @FamiliaId)
                                                    WHEN @DependenteId = 4 THEN 'Bebê ' + (SELECT Conjuge FROM dbo.Familia WHERE Id = @FamiliaId)
                                                    ELSE 'Dependente ' + (SELECT Conjuge FROM dbo.Familia WHERE Id = @FamiliaId)
                                                  END;
                              
                            -- Gera uma idade aleatória para o dependente menor de 18 anos
                            SET @IdadeDependente = CAST(RAND() * 17 + 1 AS INT);
        
                            -- Inserção do dependente na tabela Pessoa
                            INSERT INTO dbo.Pessoa (FamiliaId, Nome, Idade)
                            VALUES (@FamiliaId, @NomeDependente, @IdadeDependente);
        
                            SET @DependenteId = @DependenteId + 1;
                        END;
    
                        SET @FamiliaId = @FamiliaId + 1;
                    END;
            ");
    }
}