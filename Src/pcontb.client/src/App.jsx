import NavbarContainer from './Components/Common/NavbarContainer';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './Views/Home/HomeView';
import Dashboard from './Views/Dashboard/DashboardView';

function App() {
	return (
		<Router>
			<NavbarContainer />
			<Routes>
				<Route path="home" element={<Home />} />
				<Route path="dashboard" element={<Dashboard />} />
			</Routes>
		</Router>
	);
}

export default App;
