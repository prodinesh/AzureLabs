FROM mysql as base
ENV MYSQL_ROOT_PASSWORD=Azure123
ENV MYSQL_DATABASE=experts

COPY mysqlscript.sql /docker-entrypoint-init.d/
