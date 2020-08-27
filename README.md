#### Poc Elastic Search com Nest e ElasticSearch.Net

##### Criando Ambiente com Docker
Usei o docker tool box por que meu sistema tem suporte a HiperV ( windows 10 Home)
- Criar a rede para ambos
```javascript
`$ docker network create esnetwork --driver=bridge`
```

- Com a rede configurada, execute os contêineres Elasticsearch e Kibana:
```javascript
$ docker run -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" --name elasticsearch -d --network esnetwork docker.elastic.co/elasticsearch/elasticsearch:6.3.0
```
```javascript
$ docker run -p 5601:5601 --name kibana -d --network esnetwork docker.elastic.co/kibana/kibana:6.3.0
```

#### Depois eu conto o resto dos detalhes do projeto

#### Comparando banco de dados relacional com elastic search

##### Termos

| BD Relacional     | Elastic Search |
| ------------------|----------------|
| Instância         | Index          |
| Tabela            | Type           |
| Schema	        | Mapping        |
| Tupla (Registro)	| Documento      |
| Coluna	        | Atributo       |
| Partição	        | Shard          |


#### Dados

|Ação	|BD Relacional	|Elastic Search|
|-------|---------------|--------------|
|Verificando se registro/documento existe	|select 1 from TYPE where exists id = ID;	HEAD /index/type/id|
|Lendo um registro	|select * from TYPE where id = ID;	|GET /index/type/id||
|Inserção | insert into TYPE (id, atributo1, atributo2) values (ID, valor1, valor2); ||
|Atualização de registros/documentos inteiros |update TYPE set atributo1 = valor1, atributo2 = valor2 where id = ID; |PUT /index/type/id|
| - Inserção										| - insert into TYPE (id, atributo1, atributo2) values (ID, valor1, valor2);|POST /index/type/id|
| - Inserção com geração automática de ID			| - insert into TYPE (atributo1, atributo2) values (valor1, valor2);|POST /index/type|
| - Atualização parcial de registros/documentos		| - update TYPE set atributo1 = valor1 where id = ID;|POST /index/type/id/_update|


Uira Peixoto
