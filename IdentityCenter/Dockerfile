FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000
COPY . /app
#RUN ln -sf /usr/share/zoneinfo/Asia/Shanghai /etc/localtime
#RUN echo "Asia/shanghai" > /etc/timezone
#RUN apt-get -reconfigure -f noninteractive tzdata

ENV TZ=Asia/Shanghai 
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone��

ENTRYPOINT ["dotnet", "IdentityCenter.dll"]