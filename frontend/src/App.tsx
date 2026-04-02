import { Outlet } from "react-router-dom";
import Navbar from "./Components/Navbar/Navbar";
import { ToastContainer } from "react-toastify";
import { UserProvider } from "./Context/useAuth";

import "react-toastify/dist/ReactToastify.css";
import "./App.css";

function App() {
  return (
    <UserProvider>
      <Navbar />
      <Outlet />
      <ToastContainer />
    </UserProvider>
  );
}

export default App;