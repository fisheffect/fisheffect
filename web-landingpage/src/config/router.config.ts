import DefaultPanelLayout from '@/views/layouts/DefaultPanelLayout.vue'
import LandingPageView from '@/views/LandingPageView.vue'
import TutorialsView from '@/views/TutorialsView.vue'

/*
 *** SET HERE THE ROUTER OPTIONS ***
 */
export const router = {
  routes: [
    {
      path: '/',
      component: DefaultPanelLayout,
      children: [
        {
          path: '/',
          name: 'landingPage',
          component: LandingPageView,
        },
        {
          path: '/tutorials',
          name: 'tutorials',
          component: TutorialsView,
        },
      ],
    },
    {path: '*', redirect: '/'},
  ],
}
