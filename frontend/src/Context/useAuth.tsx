import {
  createContext,
  useContext,
  useEffect,
  useState,
  ReactNode,
} from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { toast } from "react-toastify";

import { UserProfile } from "../Models/User";
import { loginAPI, registerAPI } from "../Services/AuthService";

type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  registerUser: (
    email: string,
    username: string,
    password: string
  ) => Promise<void>;
  loginUser: (username: string, password: string) => Promise<void>;
  logout: () => void;
  isLoggedIn: () => boolean;
};

type Props = {
  children: ReactNode;
};

const UserContext = createContext<UserContextType | undefined>(undefined);

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate();

  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<UserProfile | null>(null);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    const storedToken = localStorage.getItem("token");

    if (storedUser && storedToken) {
      setUser(JSON.parse(storedUser));
      setToken(storedToken);
      axios.defaults.headers.common["Authorization"] = `Bearer ${storedToken}`;
    }

    setIsReady(true);
  }, []);

  const registerUser = async (
    email: string,
    username: string,
    password: string
  ): Promise<void> => {
    try {
      const res = await registerAPI(email, username, password);

      const token = res.token;
      const userObj: UserProfile = {
        userName: res.userName,
        email: res.email,
      };

      localStorage.setItem("token", token);
      localStorage.setItem("user", JSON.stringify(userObj));

      setToken(token);
      setUser(userObj);
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;

      toast.success("Register Success!");
      navigate("/search");
    } catch (error: any) {
      console.log("REGISTER ERROR:", error?.response?.data || error.message);
      toast.warning("Register failed.");
    }
  };

  const loginUser = async (
    username: string,
    password: string
  ): Promise<void> => {
    try {
      const res = await loginAPI(username, password);

      const token = res.token;
      const userObj: UserProfile = {
        userName: res.userName,
        email: res.email,
      };

      localStorage.setItem("token", token);
      localStorage.setItem("user", JSON.stringify(userObj));

      setToken(token);
      setUser(userObj);
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;

      toast.success("Login Success!");
      navigate("/search");
    } catch (error: any) {
      console.log("LOGIN ERROR:", error?.response?.data || error.message);
      toast.warning("Login failed.");
    }
  };

  const isLoggedIn = (): boolean => {
    return !!token && !!user;
  };

  const logout = (): void => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setToken(null);
    delete axios.defaults.headers.common["Authorization"];
    navigate("/");
  };

  return (
    <UserContext.Provider
      value={{ user, token, registerUser, loginUser, logout, isLoggedIn }}
    >
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = (): UserContextType => {
  const context = useContext(UserContext);

  if (!context) {
    throw new Error("useAuth must be used within a UserProvider");
  }

  return context;
};