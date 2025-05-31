# ManoelStore API - By Natanael Sebastião 

Microserviço para processamento de pedidos e cálculo de embalagens, construído com .NET (cumprindo o desafio técnico), SQL Server e Docker.

## Pré-requisitos!

- [Docker](https://www.docker.com/get-started) e Docker Compose instalados.
- (Opcional) [.NET SDK](https://dotnet.microsoft.com/download)

## Como Rodar a Aplicação com Docker Compose

1.  **Clone o Repositório**

    ```bash
    # git clone <URL_DO_SEU_REPOSITORIO_AQUI>
    # cd <NOME_DA_PASTA_DO_PROJETO>
    ```

2.  **Configure a Senha do SQL Server (Obrigatório)**:
    Na raiz do projeto, crie um arquivo chamado `password.env` com o seguinte conteúdo:

    ***

    ## SA_PASSWORD=DigitaSuaSenhaAqui

        *Lembre-se de adicionar `.env` ao seu arquivo `.gitignore`!*

3.  **Construa e Inicie os Contêineres**:
    No diretório raiz do projeto (onde está o `docker-compose.yml`), execute:

    ```bash
    docker-compose up --build
    ```

    - `--build` força a reconstrução da imagem da API. Pode ser omitido depois se o código não mudar.
    - Para rodar em background: `docker-compose up -d --build`.

4.  **Acessando a API**:

    - **Swagger UI**: Abra seu navegador e vá para [http://localhost:8080](http://localhost:8080).
    - O endpoint de processamento de pedidos é `POST /api/Orders`.

5.  **Exemplo de Payload para `POST /api/Orders`**:

    ```json
    [
      {
        "orderId": 123,
        "products": [
          {
            "height": 10,
            "width": 10,
            "length": 10
          },
          {
            "height": 70,
            "width": 40,
            "length": 30
          }
        ]
      },
      {
        "orderId": 456,
        "products": [
          {
            "height": 100,
            "width": 100,
            "length": 100
          }
        ]
      }
    ]
    ```

6.  **Parando os Contêineres**:
    Pressione `Ctrl+C` no terminal. Se em detached mode, use:
    ```bash
    docker-compose down
    ```
    Para remover volumes (apaga dados do banco): `docker-compose down -v`.

## Migrations (Desenvolvimento)

Ao fazer alterações nos modelos (`Order.cs`, `Product.cs`) que afetam o esquema do banco:

1.  Certifique-se de ter as ferramentas do EF Core (`dotnet tool install --global dotnet-ef` ou `dotnet tool update --global dotnet-ef`).
2.  No terminal, na raiz do projeto:
    ```bash
    dotnet ef migrations add NomeDaSuaMigration -o Data/Migrations
    ```
3.  As migrations serão aplicadas automaticamente quando a API iniciar via Docker. Se rodando localmente, você pode precisar rodar `dotnet ef database update`.
