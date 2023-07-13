import { createRouter, createWebHashHistory } from 'vue-router'
import VtfContent from './../layouts/VtfContent'
import ListDevice from './../views/ListDevice'

const routes = [
    // {   path: '/',  component: App    },
    {   path: '/devices/:id', component: VtfContent  },
    {   path: '/devices', component: ListDevice  }
]
const router = createRouter ({
    history: createWebHashHistory(process.env.BASE_URL),
    routes
})

export default router