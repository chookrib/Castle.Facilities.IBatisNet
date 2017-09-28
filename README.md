# Castle.Facilities.IBatisNet

This project base on IBatisNet.DataMapper-1.6.2, improving and adding some fetures.

## Improving and Fetures

1. Update IBatisNet target .NET Framework to 4.5, remove .NET Framework 1.0 compile support.
2. Use nuget update dependent packages to lastest version, include Castle.Core, Castle.Windsor, log4net and NUnit.
3. Add IBatisNet.DataMapper nullable enum support.

Origin IBatisNet.DataMapper 1.6.2 not support nullable enum, only through custom TypeHandler to convert. Now can define nullable enum property dircet and not need config extra TypeHandler in resultMap.
4. Add IBatisNet.DataMapper &lt;sqlMaps&gt; support &lt;include&gt;.

If there are too many sqlMap files, it will be messy to write in the same file, which can be split into different files using the <include> tag for easy management. Such as:
```
<sqlMaps>
    <include resource="IBatisNet.SqlMaps.Module1.config"/>
    <include resource="IBatisNet.SqlMaps.Module2.config"/>
    <include resource="IBatisNet.SqlMaps.Module3.config"/>
</sqlMaps>
```
5. New project Castle.Facilities.IBatisNet,  instead of old Castle.Facilities.IBatisNetIntegration. Provide Castle integration IBatisNet.DataMapper capabilities, support automatic transaction.

Integration config :
```
<configuration>
    <facilities>
        <facility id="ibatis" type="Castle.Facilities.IBatisNet.IBatisNetFacility, Castle.Facilities.IBatisNet">
            <sqlMap id="sqlMap" config="IBatisNet.config"/>
        </facility>
    </facilities>
</configuration>
```
Automatic transaction config :
```
[Transactional]
public class TransactionalClass
{
    [Transaction]
    public void virtual TransactionMethod()
    {
        ...
    }
}
```
Note : If class not implement interface, method must marked as virtual.

## How to run tests
1. Create database and run "\Tests\database\tables.sql" to create tables.
2. Open "\Tests\IBatisNet.Properties.config" and config database connection string.
3. Run tests.


