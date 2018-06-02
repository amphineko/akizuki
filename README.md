# Akizuki Bootloader & Core

This repository contains the Bootloader and Core part of Akizuki. 

Bootloader and Core should be able run standalone without Infrastructure (AkizukiChan.Infrastructure) and any other extensions.

## Data Object Concepts

-   **Status**

    Every action and messages can be abstracted as a **Status**.

    **PreroutingHook**s can add features (e.g unified user identifier) or filter invalid and unauthorized **Status**es out.  
    For example, AkizukiChan.Infrastructure will tag every user found in **Status** and provide a persistent storage of each user to **Handler**s for state-ful conversation.

    **Vendor**s may have their derived **Status** classes to extend and add their special properties in order to let **Handler**s know detailed information.

## Extension Concepts

-   **Extension**

    **Extension**s act as plugins in a Akizuki instance.  
    To extends features for Akizuki, derives your **Extension** classes from **Handler** or **Vendor**.  
    Anyway, **Extension**s can still work independently by calling **OutboundRouter** without handling incoming statuses.

-   **Handler**

    **Handler**s will be called sequentially as the orders defined in **Configuration**, and asked if the **Handler** handled the status.  
    Unhandled statuses will be passed to the next **Handler**.
    **Handler**s may call **OutboundRouter** to make a reply or send something to notify other users.  
    However, a **Handler** can still send statuses even if handled nothing, like a plain **Extension**. For example, delayed messages caused by a slow job or messages triggered by a timer.

-   **Vendor**

    **Vendor**s are the bridges connect Akizuki to other platforms, from social networks to instant messaging providers.  
    Incoming statuses are routed by **InboundRouter** to **PreroutingHook**s and **Handler**s. And a **Vendor** sends statuses to connected platforms committed by **Extension**s and **Handler**s.
    