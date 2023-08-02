import AuthContext from 'Context'
import { useContext } from 'react'

export default function useAuth() {
  return useContext(AuthContext)
}
