FROM mysql as base
ENV MYSQL_ROOT_PASSWORD=Azure123
ENV MYSQL_DATABASE=experts

COPY mysqlBooksScript.sql /docker-entrypoint-initdb.d/
