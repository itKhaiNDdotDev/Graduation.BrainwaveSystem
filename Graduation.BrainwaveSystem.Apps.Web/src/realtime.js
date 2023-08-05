import { HubConnectionBuilder } from '@aspnet/signalr'
import * as signalR from '@aspnet/signalr'

class Realtime {
    constructor() {
        this.client = new HubConnectionBuilder()
        .withUrl(process.VUE_APP_BASE_URL_SOCKET + "notificationhub",{ skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
            accessTokenFactory: () => localStorage.getItem('token')})
        .build()
    }
    start(){
        this.client.start()
    }
}

export default new Realtime();