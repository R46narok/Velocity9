import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import reportWebVitals from './reportWebVitals';
import {Provider} from "react-redux";
import {
    BrowserRouter,
    Routes,
    Route
} from "react-router-dom"
import {store} from "./helpers/store";
import {Login} from "./routes/Login/Login";
import NavigationBar from "./components/NavigationBar";
import Footer from "./components/Footer";
import {Register} from "./routes/Register/Register";
import {Home} from "./routes/Home/Home";
import {Muscle } from "./routes/Muscles/Muscle";
import {MuscleGroup} from "./routes/Muscles/Group/MuscleGroup";


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
      <Provider store={store}>

          <BrowserRouter>
              <NavigationBar active="Home"/>
              <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="profile/login" element={<Login/>}/>
                <Route path="profile/register" element={<Register/>}/>
                <Route path="muscle" element={<Muscle/>}/>
                  <Route path="muscle/group" element={<MuscleGroup/>}/>
              </Routes>
              <Footer/>
          </BrowserRouter>

      </Provider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
