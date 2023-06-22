import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import {
  createBrowserRouter,
  RouterProvider,
  createRoutesFromElements,
  Route,
} from 'react-router-dom'

import Layout from './Layout/Layout.jsx';

//Pages
import CreateEvent from './Pages/CreateEvent.jsx';
import Dashboard from './Pages/Dashboard';
import Home from './Pages/Home';
import JoinedEvents from './Pages/JoinedEvents';
import Login from './Pages/Login';
import Signup from './Pages/Signup';
import Events from './Pages/Events';

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


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

