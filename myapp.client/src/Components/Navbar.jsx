import { Link } from 'react-router-dom';

export function Navbar(){
    return (
        <>
            <Link to="/">
                <button>
                    Get All Dachas
                </button>
            </Link>
            <Link to="/GetDachaById">
                <button>
                    Get Dacha By Id
                </button>
            </Link>
        </>
    )
}