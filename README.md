# DataBenchmark

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/FelipeLaus/DataBenchmark)

Uma suíte de benchmarks para comparar o desempenho e o consumo de memória de diferentes estruturas de dados em .NET. O projeto oferece uma análise clara e objetiva, ideal para decisões de arquitetura e otimização de código.

## Tecnologias Utilizadas
- **.NET 10**
- **C# 14**
- **BenchmarkDotNet** (para medição de performance)

## Recursos
- Comparação de inserção única entre `ListUnique` e `HashSetUnique`.
- Comparação de busca entre `List`, `Dictionary` e `SortedDictionary`.
- Medição precisa de tempo (milissegundos e microssegundos) e alocação de memória.
- Geração de relatórios em tabela com análise de performance relativa.

## Requisitos
- .NET 10 SDK
- Visual Studio 2026 (ou superior) ou VS Code

> **Nota:** Ao usar o Visual Studio, certifique-se de que o projeto está configurado para o Target Framework correto em __Project Properties > Target Framework__.

## Como Usar

### 1. Clone o repositório

### 2. Compile o projeto
Execute o seguinte comando no terminal:

Ou, se preferir, abra a solução no Visual Studio e pressione __Build > Build Solution__.

### 3. Execute os benchmarks
Para rodar os benchmarks, utilize o comando:

O `Program.cs` já está configurado para executar os exemplos:
- `CompareInsertUnique(int iterations)` — compara a performance de inserções únicas.
- `CompareSearchDataStructures(int totalUsers)` — compara a performance de buscas e mede o consumo de memória.

## Exemplo de Saída
A execução dos benchmarks gera uma tabela formatada com os resultados, similar a esta:

| Operação          | Tempo (ms) | Tempo (µs) | Complexidade | Memória (bytes) | Baseline |
|-------------------|------------|------------|--------------|-----------------|----------|
| List.Contains     | 2.458,1    | 2.458.100  | O(n)         | 80              | 1.00x    |
| Dictionary.Get    | 0,002      | 2          | O(1)         | 48              | 0.00x    |
| SortedDictionary  | 0,005      | 5          | O(log n)     | 120             | 0.00x    |

*Os valores são meramente ilustrativos.*

## Estrutura do Projeto
- `Program.cs` — Ponto de entrada da aplicação que executa os benchmarks.
- `Benchmark.cs` — Contém a lógica dos benchmarks e a formatação dos resultados.
- `User.cs` — Modelo de dados (`record`) utilizado nos testes de busca.
- `Unique/` — Implementações de exemplo (`ListUnique` e `HashSetUnique`).

## Como Contribuir
Contribuições são sempre bem-vindas!

1. Faça um fork deste repositório.
2. Crie uma nova branch com um nome descritivo para sua feature (`git checkout -b minha-feature`).
3. Adicione testes para suas alterações, quando aplicável.
4. Faça o commit de suas mudanças (`git commit -m 'Adiciona nova feature'`).
5. Envie para a branch original (`git push origin minha-feature`).
6. Abra um Pull Request detalhando suas modificações.

Considere adicionar ou atualizar o arquivo `CONTRIBUTING.md` com regras de estilo de código e diretrizes para execução de testes.

## Licença
Este projeto está sob a licença MIT. Consulte o arquivo `LICENSE` para mais detalhes.