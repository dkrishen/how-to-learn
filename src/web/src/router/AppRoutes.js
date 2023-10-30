import Topic from "../pages/Topic"
import Topics from "../pages/Topics"
import Section from "../pages/Section"
import Sections from "../pages/Sections"
import Main from "../pages/Main"

export const AppRoutes = [
    {path: '/topics', component: Topics, exact: true},
    {path: '/topics/:id', component: Topic, exact: false},
    {path: '/sections', component: Sections, exact: true},
    {path: '/sections/:id', component: Section, exact: false},
    {path: '/', component: Main, exact: true},
]   