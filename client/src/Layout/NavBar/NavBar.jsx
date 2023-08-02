import './NavBar.css'
import { useState } from 'react'
import { Link, NavLink } from 'react-router-dom'

import { FaBars, FaTimes } from 'react-icons/fa'
import { AuthorizedRender, UnAuthorizedRender } from 'Components'
import useAuth from 'Hooks/useAuth'
import { useNavigate } from 'react-router-dom';

export default function NavBar() {

  const [isOpen,setIsOpen] = useState(false)
  const { setUser, user } = useAuth()
  const navigate = useNavigate();

  
  const logoutHandler= async () =>{
      try {
        const response = await fetch('https://localhost:7019/api/logout', {
          method: 'POST',
          credentials:"include"
        });
        if (response.ok) {
          setUser(null);
          localStorage.removeItem('user');
          localStorage.clear();
          document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
          navigate('/');
        } else {
          console.error('Failed to logout.');
        }
      } catch (error) {
        console.error('An error occurred while trying to logout.', error);
      }

  }

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
            <Link to='login' className='nav-link' onClick={logoutHandler}>Logout</Link>
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
