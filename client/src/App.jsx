import { 
    Route,
    createBrowserRouter,
    createRoutesFromElements,
    RouterProvider,
} from 'react-router-dom'

import { Layout } from 'Layout'

import {
  CreateEvent,
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
        <Route path='/createvent' element={<CreateEvent />}/>
        <Route path='/dashboard' element={< Dashboard />}/>
        <Route path='/events' element={< Events />}/>
        <Route path='/home' element={< Home />}/>      
        <Route path='/joinedevents' element={< JoinedEvents/>}/>
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
