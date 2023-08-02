import { 
    Route,
    createBrowserRouter,
    createRoutesFromElements,
    RouterProvider,
} from 'react-router-dom'

import AuthContext from 'Context'

import './Layout.css'
import './Pages/index.css'
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
import { useState } from 'react'

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

  const [user,setUser] = useState(null)

  return (
    <AuthContext.Provider value={{ user,setUser }}>
      <RouterProvider router={router} />
    </AuthContext.Provider>
  )
}
