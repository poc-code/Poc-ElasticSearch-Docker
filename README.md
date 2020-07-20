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
