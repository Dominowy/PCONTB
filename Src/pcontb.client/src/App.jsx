import NavbarContainer from './Components/Common/NavbarContainer';
import { BrowserRouter as Routes, Route } from 'react-router-dom';
import Home from './Views/Home/HomeView';
import Dashboard from './Views/Dashboard/DashboardView';

function App() {
	<>
		<div>
			<div>
				<Routes>
					<Route path="home" element={<Home />} />
					<Route path="dashboard" element={<Dashboard />} />
				</Routes>
			</div>
			<NavbarContainer />
		</div>
	</>;
	return;
}

export default App;
