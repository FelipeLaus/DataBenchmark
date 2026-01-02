# DataBenchmark

Pequena suíte de benchmarks para comparar desempenho e consumo de memória entre diferentes estruturas de dados em .NET.

## Recursos
- Comparação de inserção única entre `ListUnique` e `HashSetUnique`.
- Comparação de busca entre `List`, `Dictionary` e `SortedDictionary`.
- Medição de tempo (ms e µs) e consumo de memória.
- Saída formatada em tabela com performance relativa ao baseline.

## Requisitos
- .NET 10 SDK
- C# 14
- Visual Studio (ex.: VS 2022/2026) ou VS Code

> Se usar Visual Studio, verifique o alvo do framework em __Project Properties > Target Framework__.

## Como compilar
No diretório do projeto execute:

Ou abra no Visual Studio e pressione __Build > Build Solution__.

## Como executar
Executar pelo terminal:

O `Program.cs` já executa exemplos:
- `CompareInsertUnique(int iterations)` — compara inserção única.
- `CompareSearchDataStructures(int totalUsers)` — compara buscas e mede memória.

## Exemplo de saída
A execução imprime uma tabela com colunas: operação, tempo (ms), tempo (µs), complexidade, memória (bytes) e performance relativa ao baseline.

## Estrutura do projeto
- `Program.cs` — ponto de entrada.
- `Benchmark.cs` — lógica de benchmark e impressão de resultados.
- `User.cs` — modelo usado nos testes de busca.
- `Unique/` — implementações de `ListUnique` e `HashSetUnique` (exemplo).

## Como contribuir
1. Faça fork do repositório.
2. Crie uma branch com nome descritivo.
3. Adicione testes quando aplicável.
4. Abra um PR descrevendo a mudança.

Considere adicionar ou atualizar `CONTRIBUTING.md` com regras de estilo e execução de testes.

## Licença
MIT — ver o arquivo `LICENSE` para detalhes.