import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { NavLink, Link } from 'react-router-dom';

function NavbarContainer() {
	return (
		<Navbar expand="lg" bg="dark" data-bs-theme="dark">
			<Container>
				<Navbar.Brand as={Link} to="/home">
					PCONTB
				</Navbar.Brand>
				<Navbar.Toggle aria-controls="basic-navbar-nav" />
				<Navbar.Collapse id="basic-navbar-nav">
					<Nav className="me-auto">
						<Nav.Link as={NavLink} to="/home">
							Home
						</Nav.Link>
						<Nav.Link as={NavLink} to="/dashboard">
							Dashboard
						</Nav.Link>
					</Nav>
				</Navbar.Collapse>
			</Container>
		</Navbar>
	);
}

export default NavbarContainer;
