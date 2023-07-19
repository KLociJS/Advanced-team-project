import { 
    Route,
    createBrowserRouter,
    createRoutesFromElements,
    RouterProvider,
} from 'react-router-dom'

import './Layout.css'
import './Typography.css'

import { Layout } from 'Layout'

import {
  CreateEvent,
  CreatedEvents,
  Dashboard,
  Events,
  Home,
  JoinedEvents,
  Login,
  Signup,
} from 'Pages'

const router = createBrowserRouter(
    createRoutesFromElements(
      <Route path='/' element={<Layout />}>
        <Route index element={< Home />}/>      
        <Route path='/events' element={< Events />}/>
        <Route path='/dashboard' element={< Dashboard />}/>
        <Route path='/creat-event' element={<CreateEvent />}/>
        <Route path='/joined-events' element={< JoinedEvents/>}/>
        <Route path='/created-events' element={<CreatedEvents />} />
        <Route path='/login' element={< Login/>}/>
        <Route path='/signup' element={< Signup/>}/>
      </Route>
    )
  )

export default function App() {
  return (
    <RouterProvider router={router}>
        <div>App</div>
    </RouterProvider>
  )
}
