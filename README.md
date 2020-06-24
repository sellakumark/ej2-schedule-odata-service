# ej2-schedule-odata-service

The OData Service has been refered in below format in sample side

```
eventSettings: {
    dataSource: new DataManager({
        url: "https://localhost:44337/odata/",
        adaptor: new ODataAdaptor(),
        crossDomain: true
    }),
    query: new Query().from("Schedule")
}
```