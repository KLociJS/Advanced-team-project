import './NavBar.css'
import { useState } from 'react'
import { Link, NavLink } from 'react-router-dom'

import { FaBars, FaTimes } from 'react-icons/fa'
import { AuthorizedRender, UnAuthorizedRender } from 'Components'
import useAuth from 'Hooks/useAuth'

export default function NavBar() {

  const [isOpen,setIsOpen] = useState(false)
  const { setUser, user } = useAuth()

  return (
    <nav className='nav-bar'>
      <ul className={`nav-ul ${isOpen ? 'active' : ''}`}>
        <li className='nav-li'>
          <NavLink to='/' className='nav-link' onClick={()=>setIsOpen(false)}>Home</NavLink>
        </li>
        <li className='nav-li'>
          <NavLink to='events' className='nav-link' onClick={()=>setIsOpen(false)}>Events</NavLink>
        </li>
        <AuthorizedRender>
          <li className='nav-li'>
            <NavLink to='dashboard' className='nav-link' onClick={()=>setIsOpen(false)}>Events Dashboard</NavLink>
          </li>
          <li className='nav-li align-right'>
            <p className='nav-user'>Logged in as {user?.userName}</p>
          </li>
          <li className='nav-li'>
            <Link to='login' className='nav-link' onClick={()=>setUser(null)}>Logout</Link>
          </li>
        </AuthorizedRender>
        <UnAuthorizedRender>
          <li className='nav-li align-right'>
            <NavLink to='login' className='nav-link' onClick={()=>setIsOpen(false)}>Login</NavLink>
          </li>
          <li className='nav-li'>
            <NavLink to='signup' className='nav-link' onClick={()=>setIsOpen(false)}>Signup</NavLink>
          </li>
        </UnAuthorizedRender>
      </ul>
      {isOpen ? 
      <FaTimes onClick={()=>setIsOpen(false)} className='hamburger-icon' size={'30px'} /> :
      <FaBars onClick={()=>setIsOpen(true)} className='hamburger-icon' size={'30px'}/> 
      }
    </nav>
  )
}
