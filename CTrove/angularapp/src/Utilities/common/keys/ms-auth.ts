import { MsalInterceptorConfiguration } from "@azure/msal-angular"
import { BrowserCacheLocation, Configuration, IPublicClientApplication, InteractionType, PopupRequest, PublicClientApplication } from "@azure/msal-browser"
import { environment } from "src/environments/environment"

const isIE = /msie\s|trident\//i.test(window.navigator.userAgent)

export const authConfiguration: Configuration = {
    auth: {
        clientId: environment.clientId,
        redirectUri: environment.serverUrl,
        authority: environment.tenantId
    },
    cache: {
        cacheLocation: BrowserCacheLocation.LocalStorage,
        storeAuthStateInCookie: isIE
    }
} 

export const AUTH_REQUEST: PopupRequest = {
    scopes: [ environment.scope ,'openid','profile','offline_access']
}

export function MSAL_InstanceFactory() : IPublicClientApplication {
    return new PublicClientApplication(authConfiguration)
}

export function MSALInterceptorConfigFactory() : MsalInterceptorConfiguration {
    const protectedResourceMap = new Map<string, Array<string>>();
    return {
        interactionType: InteractionType.Redirect,
        protectedResourceMap
}
} 