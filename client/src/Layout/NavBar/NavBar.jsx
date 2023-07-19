import './NavBar.css'
import { useState } from 'react'
import { NavLink } from 'react-router-dom'

import { FaBars, FaTimes } from 'react-icons/fa'

export default function NavBar() {

  const [isOpen,setIsOpen] = useState(false)

  return (
    <nav className='nav-bar'>
      <ul className={`nav-ul ${isOpen ? 'active' : ''}`}>
        <li className='nav-li'>
          <NavLink to='/' className='nav-link' onClick={()=>setIsOpen(false)}>Home</NavLink>
        </li>
        <li className='nav-li'>
          <NavLink to='events' className='nav-link' onClick={()=>setIsOpen(false)}>Events</NavLink>
        </li>
        <li className='nav-li'>
          <NavLink to='dashboard' className='nav-link' onClick={()=>setIsOpen(false)}>Events Dashboard</NavLink>
        </li>
        <li className='nav-li align-right'>
          <NavLink to='login' className='nav-link' onClick={()=>setIsOpen(false)}>Login</NavLink>
        </li>
        <li className='nav-li'>
          <NavLink to='signup' className='nav-link' onClick={()=>setIsOpen(false)}>Signup</NavLink>
        </li>
      </ul>
      {isOpen ? 
      <FaTimes onClick={()=>setIsOpen(false)} className='hamburger-icon' size={'30px'} /> :
      <FaBars onClick={()=>setIsOpen(true)} className='hamburger-icon' size={'30px'}/> 
      }
    </nav>
  )
}
