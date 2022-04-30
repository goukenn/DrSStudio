

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XLib.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XLib.cs
*/
//XLIB.cs
//implement a libx11 function 
using System;
using System.Text ;
using System.Runtime.InteropServices;
namespace IGK.GLLib
{
	public class XLib
	{
		 [StructLayout(LayoutKind.Sequential , Pack=1)]
		 public struct XFONTStruct{
	internal IntPtr  ext_data;		/* hook for extension to hang data */
	internal IntPtr  fid;			/* Font id for this font */
	internal int direction;		/* hint about the direction font is painted */
	internal int  min_char_or_byte2;	/* first character */
	internal int  max_char_or_byte2;	/* last character */
	internal int  min_byte1;		/* first row that exists */
	internal int max_byte1;		/* last row that exists */
	internal bool  all_chars_exist;		/* flag if all characters have nonzero size */
	internal char  default_char;		/* char to print for undefined character */
	internal int n_properties;		/* how many properties there are */
	internal IntPtr properties;		/* pointer to array of additional properties */
	internal XCharStruct min_bounds;		/* minimum bounds over all existing char */
	internal XCharStruct max_bounds;		/* maximum bounds over all existing char */
	internal XCharStruct per_char;		/* first_char to last_char information */
	internal int ascent;			/* logical extent above baseline for spacing */
	internal int descent;			/* logical decent below baseline for spacing */
}
		[StructLayout(LayoutKind.Sequential , Pack=1)]
		public struct XCharStruct 
		{
		}
		const string LIBNAME = "libX11";
		[DllImport(LIBNAME)] public static extern IntPtr XOpenDisplay(IntPtr h);
		[DllImport(LIBNAME)] public static extern void XCloseDisplay(IntPtr display);
		[DllImport(LIBNAME)] public static extern int XDefaultScreen(IntPtr display);
		//font function
		[DllImport(LIBNAME)] public static extern IntPtr  XLoadQueryFont(IntPtr display, string name);
		[DllImport(LIBNAME)] public static extern IntPtr  XListFonts(IntPtr display, string pattern, int maxnum, ref int num);
		[DllImport(LIBNAME)] public static extern void XFreeFontNames(IntPtr name);
		[DllImport(LIBNAME)] public static extern int XRootWindow(IntPtr display, IntPtr screen);
		 [DllImport(LIBNAME, EntryPoint = "XSynchronize")]
        public extern static IntPtr XSynchronize(IntPtr display, bool onoff);
        [DllImport(LIBNAME, EntryPoint = "XCreateWindow")]
        public extern static IntPtr XCreateWindow(IntPtr display, IntPtr parent, int x, int y, int width, int height, int border_width, int depth, int xclass, IntPtr visual, uint valuemask, IntPtr attributes);
        [DllImport(LIBNAME, EntryPoint = "XCreateSimpleWindow")]
        public extern static IntPtr XCreateSimpleWindow(IntPtr display, IntPtr parent, int x, int y, int width, int height, int border_width, UIntPtr border, UIntPtr background);
        [DllImport(LIBNAME, EntryPoint = "XMapWindow")]
        public extern static int XMapWindow(IntPtr display, IntPtr window);
        [DllImport(LIBNAME, EntryPoint = "XUnmapWindow")]
        public extern static int XUnmapWindow(IntPtr display, IntPtr window);
        [DllImport(LIBNAME, EntryPoint = "XMapSubwindows")]
        public extern static int XMapSubindows(IntPtr display, IntPtr window);
        [DllImport(LIBNAME, EntryPoint = "XUnmapSubwindows")]
        public extern static int XUnmapSubwindows(IntPtr display, IntPtr window);
        [DllImport(LIBNAME, EntryPoint = "XRootWindow")]
        public extern static IntPtr XRootWindow(IntPtr display, int screen_number);
        [DllImport(LIBNAME, EntryPoint = "XNextEvent")]
        public extern static IntPtr XNextEvent(IntPtr display, IntPtr xevent);
        [DllImport(LIBNAME)]
        public extern static int XConnectionNumber(IntPtr diplay);
        [DllImport(LIBNAME)]
        public extern static int XPending(IntPtr diplay);
        [DllImport(LIBNAME, EntryPoint = "XSelectInput")]
        public extern static IntPtr XSelectInput(IntPtr display, IntPtr window, IntPtr mask);
        [DllImport(LIBNAME, EntryPoint = "XDestroyWindow")]
        public extern static int XDestroyWindow(IntPtr display, IntPtr window);
        [DllImport(LIBNAME, EntryPoint = "XReparentWindow")]
        public extern static int XReparentWindow(IntPtr display, IntPtr window, IntPtr parent, int x, int y);
        [DllImport(LIBNAME, EntryPoint = "XMoveResizeWindow")]
        public extern static int XMoveResizeWindow(IntPtr display, IntPtr window, int x, int y, int width, int height);
        [DllImport(LIBNAME, EntryPoint = "XResizeWindow")]
        public extern static int XResizeWindow(IntPtr display, IntPtr window, int width, int height);
        [DllImport(LIBNAME, EntryPoint = "XGetWindowAttributes")]
        public extern static int XGetWindowAttributes(IntPtr display, IntPtr window, IntPtr attributes);
        [DllImport(LIBNAME, EntryPoint = "XFlush")]
        public extern static int XFlush(IntPtr display);
        [DllImport(LIBNAME, EntryPoint = "XSetWMName")]
        public extern static int XSetWMName(IntPtr display, IntPtr window, IntPtr text_prop);
        [DllImport(LIBNAME, EntryPoint = "XStoreName")]
        public extern static int XStoreName(IntPtr display, IntPtr window, string window_name);
        [DllImport(LIBNAME, EntryPoint = "XFetchName")]
        public extern static int XFetchName(IntPtr display, IntPtr window, ref IntPtr window_name);
        [DllImport(LIBNAME, EntryPoint = "XSendEvent")]
        public extern static int XSendEvent(IntPtr display, IntPtr window, bool propagate, IntPtr event_mask, IntPtr send_event);
        [DllImport(LIBNAME, EntryPoint = "XQueryTree")]
        public extern static int XQueryTree(IntPtr display, IntPtr window, out IntPtr root_return, out IntPtr parent_return, out IntPtr children_return, out int nchildren_return);
        [DllImport(LIBNAME, EntryPoint = "XFree")]
        public extern static int XFree(IntPtr data);
        [DllImport(LIBNAME, EntryPoint = "XRaiseWindow")]
        public extern static int XRaiseWindow(IntPtr display, IntPtr window);
        [DllImport(LIBNAME, EntryPoint = "XLowerWindow")]
        public extern static uint XLowerWindow(IntPtr display, IntPtr window);
        [DllImport(LIBNAME, EntryPoint = "XConfigureWindow")]
        public extern static uint XConfigureWindow(IntPtr display, IntPtr window, IntPtr value_mask, ref IntPtr values);
        [DllImport(LIBNAME, EntryPoint = "XInternAtom")]
        public extern static IntPtr XInternAtom(IntPtr display, string atom_name, bool only_if_exists);
        [DllImport(LIBNAME, EntryPoint = "XInternAtoms")]
        public extern static int XInternAtoms(IntPtr display, string[] atom_names, int atom_count, bool only_if_exists, IntPtr[] atoms);
        [DllImport(LIBNAME, EntryPoint = "XSetWMProtocols")]
        public extern static int XSetWMProtocols(IntPtr display, IntPtr window, IntPtr[] protocols, int count);
        [DllImport(LIBNAME, EntryPoint = "XGrabPointer")]
        public extern static int XGrabPointer(IntPtr display, IntPtr window, bool owner_events, IntPtr event_mask, IntPtr pointer_mode, IntPtr keyboard_mode, IntPtr confine_to, IntPtr cursor, IntPtr timestamp);
        [DllImport(LIBNAME, EntryPoint = "XUngrabPointer")]
        public extern static int XUngrabPointer(IntPtr display, IntPtr timestamp);
        [DllImport(LIBNAME, EntryPoint = "XQueryPointer")]
        public extern static bool XQueryPointer(IntPtr display, IntPtr window, out IntPtr root, out IntPtr child, out int root_x, out int root_y, out int win_x, out int win_y, out int keys_buttons);
        [DllImport(LIBNAME, EntryPoint = "XTranslateCoordinates")]
        public extern static bool XTranslateCoordinates(IntPtr display, IntPtr src_w, IntPtr dest_w, int src_x, int src_y, out int intdest_x_return, out int dest_y_return, out IntPtr child_return);
        [DllImport(LIBNAME, EntryPoint = "XGetGeometry")]
        public extern static bool XGetGeometry(IntPtr display, IntPtr window, out IntPtr root, out int x, out int y, out int width, out int height, out int border_width, out int depth);
        [DllImport(LIBNAME, EntryPoint = "XGetGeometry")]
        public extern static bool XGetGeometry(IntPtr display, IntPtr window, IntPtr root, out int x, out int y, out int width, out int height, IntPtr border_width, IntPtr depth);
        [DllImport(LIBNAME, EntryPoint = "XGetGeometry")]
        public extern static bool XGetGeometry(IntPtr display, IntPtr window, IntPtr root, out int x, out int y, IntPtr width, IntPtr height, IntPtr border_width, IntPtr depth);
        [DllImport(LIBNAME, EntryPoint = "XGetGeometry")]
        public extern static bool XGetGeometry(IntPtr display, IntPtr window, IntPtr root, IntPtr x, IntPtr y, out int width, out int height, IntPtr border_width, IntPtr depth);
        [DllImport(LIBNAME, EntryPoint = "XWarpPointer")]
        public extern static uint XWarpPointer(IntPtr display, IntPtr src_w, IntPtr dest_w, int src_x, int src_y, uint src_width, uint src_height, int dest_x, int dest_y);
        [DllImport(LIBNAME, EntryPoint = "XClearWindow")]
        public extern static int XClearWindow(IntPtr display, IntPtr window);
        [DllImport(LIBNAME, EntryPoint = "XClearArea")]
        public extern static int XClearArea(IntPtr display, IntPtr window, int x, int y, int width, int height, bool exposures);
        // Colormaps
        [DllImport(LIBNAME, EntryPoint = "XDefaultScreenOfDisplay")]
        public extern static IntPtr XDefaultScreenOfDisplay(IntPtr display);
        [DllImport(LIBNAME, EntryPoint = "XScreenNumberOfScreen")]
        public extern static int XScreenNumberOfScreen(IntPtr display, IntPtr Screen);
        [DllImport(LIBNAME, EntryPoint = "XDefaultVisual")]
        public extern static IntPtr XDefaultVisual(IntPtr display, int screen_number);
        [DllImport(LIBNAME, EntryPoint = "XDefaultDepth")]
        public extern static uint XDefaultDepth(IntPtr display, int screen_number);
        [DllImport(LIBNAME, EntryPoint = "XDefaultColormap")]
        public extern static IntPtr XDefaultColormap(IntPtr display, int screen_number);
        [DllImport(LIBNAME, EntryPoint = "XLookupColor")]
        public extern static int XLookupColor(IntPtr display, IntPtr Colormap, string Coloranem, ref IntPtr exact_def_color, ref IntPtr screen_def_color);
        [DllImport(LIBNAME, EntryPoint = "XAllocColor")]
        public extern static int XAllocColor(IntPtr display, IntPtr Colormap, ref IntPtr colorcell_def);
        [DllImport(LIBNAME, EntryPoint = "XSetTransientForHint")]
        public extern static int XSetTransientForHint(IntPtr display, IntPtr window, IntPtr prop_window);
        [DllImport(LIBNAME, EntryPoint = "XChangeProperty")]
        public extern static int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, int mode, ref IntPtr data, int nelements);
        [DllImport(LIBNAME, EntryPoint = "XChangeProperty")]
        public extern static int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, int mode, ref uint value, int nelements);
        [DllImport(LIBNAME, EntryPoint = "XChangeProperty")]
        public extern static int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, int mode, uint[] data, int nelements);
        [DllImport(LIBNAME, EntryPoint = "XChangeProperty")]
        public extern static int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, int mode, int[] data, int nelements);
        [DllImport(LIBNAME, EntryPoint = "XChangeProperty")]
        public extern static int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, int mode, IntPtr[] data, int nelements);
        [DllImport(LIBNAME, EntryPoint = "XChangeProperty")]
        public extern static int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, int mode, IntPtr atoms, int nelements);
        [DllImport(LIBNAME, EntryPoint = "XChangeProperty", CharSet = CharSet.Ansi)]
        public extern static int XChangeProperty(IntPtr display, IntPtr window, IntPtr property, IntPtr type, int format, int mode, string text, int text_length);
        [DllImport(LIBNAME, EntryPoint = "XDeleteProperty")]
        public extern static int XDeleteProperty(IntPtr display, IntPtr window, IntPtr property);
        // Drawing
        [DllImport(LIBNAME, EntryPoint = "XCreateGC")]
        public extern static IntPtr XCreateGC(IntPtr display, IntPtr window, IntPtr valuemask, ref IntPtr values);
        [DllImport(LIBNAME, EntryPoint = "XFreeGC")]
        public extern static int XFreeGC(IntPtr display, IntPtr gc);
        [DllImport(LIBNAME, EntryPoint = "XSetFunction")]
        public extern static int XSetFunction(IntPtr display, IntPtr gc, IntPtr function);
        [DllImport(LIBNAME, EntryPoint = "XSetLineAttributes")]
        public extern static int XSetLineAttributes(IntPtr display, IntPtr gc, int line_width, int line_style, int cap_style, int join_style);
        [DllImport(LIBNAME, EntryPoint = "XDrawLine")]
        public extern static int XDrawLine(IntPtr display, IntPtr drawable, IntPtr gc, int x1, int y1, int x2, int y2);
        [DllImport(LIBNAME, EntryPoint = "XDrawRectangle")]
        public extern static int XDrawRectangle(IntPtr display, IntPtr drawable, IntPtr gc, int x1, int y1, int width, int height);
        [DllImport(LIBNAME, EntryPoint = "XFillRectangle")]
        public extern static int XFillRectangle(IntPtr display, IntPtr drawable, IntPtr gc, int x1, int y1, int width, int height);
        [DllImport(LIBNAME, EntryPoint = "XSetWindowBackground")]
        public extern static int XSetWindowBackground(IntPtr display, IntPtr window, IntPtr background);
        [DllImport(LIBNAME, EntryPoint = "XCopyArea")]
        public extern static int XCopyArea(IntPtr display, IntPtr src, IntPtr dest, IntPtr gc, int src_x, int src_y, int width, int height, int dest_x, int dest_y);
        [DllImport(LIBNAME, EntryPoint = "XGetWindowProperty")]
        public extern static int XGetWindowProperty(IntPtr display, IntPtr window, IntPtr atom, IntPtr long_offset, IntPtr long_length, bool delete, IntPtr req_type, out IntPtr actual_type, out int actual_format, out IntPtr nitems, out IntPtr bytes_after, ref IntPtr prop);
        [DllImport(LIBNAME, EntryPoint = "XSetInputFocus")]
        public extern static int XSetInputFocus(IntPtr display, IntPtr window, IntPtr revert_to, IntPtr time);
        [DllImport(LIBNAME, EntryPoint = "XIconifyWindow")]
        public extern static int XIconifyWindow(IntPtr display, IntPtr window, int screen_number);
        [DllImport(LIBNAME, EntryPoint = "XDefineCursor")]
        public extern static int XDefineCursor(IntPtr display, IntPtr window, IntPtr cursor);
        [DllImport(LIBNAME, EntryPoint = "XUndefineCursor")]
        public extern static int XUndefineCursor(IntPtr display, IntPtr window);
        [DllImport(LIBNAME, EntryPoint = "XFreeCursor")]
        public extern static int XFreeCursor(IntPtr display, IntPtr cursor);
        [DllImport(LIBNAME, EntryPoint = "XCreateFontCursor")]
        public extern static IntPtr XCreateFontCursor(IntPtr display, IntPtr shape);
        [DllImport(LIBNAME, EntryPoint = "XCreatePixmapCursor")]
        public extern static IntPtr XCreatePixmapCursor(IntPtr display, IntPtr source, IntPtr mask, ref IntPtr foreground_color, ref IntPtr background_color, int x_hot, int y_hot);
        [DllImport(LIBNAME, EntryPoint = "XCreatePixmapFromBitmapData")]
        public extern static IntPtr XCreatePixmapFromBitmapData(IntPtr display, IntPtr drawable, byte[] data, int width, int height, IntPtr fg, IntPtr bg, int depth);
        [DllImport(LIBNAME, EntryPoint = "XCreatePixmap")]
        public extern static IntPtr XCreatePixmap(IntPtr display, IntPtr d, int width, int height, int depth);
        [DllImport(LIBNAME, EntryPoint = "XFreePixmap")]
        public extern static IntPtr XFreePixmap(IntPtr display, IntPtr pixmap);
        [DllImport(LIBNAME, EntryPoint = "XQueryBestCursor")]
        public extern static int XQueryBestCursor(IntPtr display, IntPtr drawable, int width, int height, out int best_width, out int best_height);
        [DllImport(LIBNAME, EntryPoint = "XQueryExtension")]
        public extern static int XQueryExtension(IntPtr display, string extension_name, ref int major, ref int first_event, ref int first_error);
        [DllImport(LIBNAME, EntryPoint = "XWhitePixel")]
        public extern static IntPtr XWhitePixel(IntPtr display, int screen_no);
        [DllImport(LIBNAME, EntryPoint = "XBlackPixel")]
        public extern static IntPtr XBlackPixel(IntPtr display, int screen_no);
        [DllImport(LIBNAME, EntryPoint = "XGrabServer")]
        public extern static void XGrabServer(IntPtr display);
        [DllImport(LIBNAME, EntryPoint = "XUngrabServer")]
        public extern static void XUngrabServer(IntPtr display);
        [DllImport(LIBNAME, EntryPoint = "XGetWMNormalHints")]
        public extern static void XGetWMNormalHints(IntPtr display, IntPtr window, ref IntPtr hints, out IntPtr supplied_return);
        [DllImport(LIBNAME, EntryPoint = "XSetWMNormalHints")]
        public extern static void XSetWMNormalHints(IntPtr display, IntPtr window, ref IntPtr hints);
        [DllImport(LIBNAME, EntryPoint = "XSetZoomHints")]
        public extern static void XSetZoomHints(IntPtr display, IntPtr window, ref IntPtr hints);
        [DllImport(LIBNAME, EntryPoint = "XSetWMHints")]
        public extern static void XSetWMHints(IntPtr display, IntPtr window, ref IntPtr wmhints);
        [DllImport(LIBNAME, EntryPoint = "XGetIconSizes")]
        public extern static int XGetIconSizes(IntPtr display, IntPtr window, out IntPtr size_list, out int count);
        [DllImport(LIBNAME, EntryPoint = "XSetErrorHandler")]
        public extern static IntPtr XSetErrorHandler(IntPtr error_handler);
        [DllImport(LIBNAME, EntryPoint = "XGetErrorText")]
        public extern static IntPtr XGetErrorText(IntPtr display, byte code, StringBuilder buffer, int length);
        [DllImport(LIBNAME, EntryPoint = "XInitThreads")]
        public extern static int XInitThreads();
        [DllImport(LIBNAME, EntryPoint = "XConvertSelection")]
        public extern static int XConvertSelection(IntPtr display, IntPtr selection, IntPtr target, IntPtr property, IntPtr requestor, IntPtr time);
        [DllImport(LIBNAME, EntryPoint = "XGetSelectionOwner")]
        public extern static IntPtr XGetSelectionOwner(IntPtr display, IntPtr selection);
        [DllImport(LIBNAME, EntryPoint = "XSetSelectionOwner")]
        public extern static int XSetSelectionOwner(IntPtr display, IntPtr selection, IntPtr owner, IntPtr time);
        [DllImport(LIBNAME, EntryPoint = "XSetPlaneMask")]
        public extern static int XSetPlaneMask(IntPtr display, IntPtr gc, IntPtr mask);
        [DllImport(LIBNAME, EntryPoint = "XSetForeground")]
        public extern static int XSetForeground(IntPtr display, IntPtr gc, UIntPtr foreground);
        [DllImport(LIBNAME, EntryPoint = "XSetBackground")]
        public extern static int XSetBackground(IntPtr display, IntPtr gc, UIntPtr background);
        [DllImport(LIBNAME, EntryPoint = "XBell")]
        public extern static int XBell(IntPtr display, int percent);
        [DllImport(LIBNAME, EntryPoint = "XChangeActivePointerGrab")]
        public extern static int XChangeActivePointerGrab(IntPtr display, IntPtr event_mask, IntPtr cursor, IntPtr time);
        [DllImport(LIBNAME, EntryPoint = "XFilterEvent")]
        public extern static bool XFilterEvent(IntPtr xevent, IntPtr window);
        [DllImport(LIBNAME)]
        public extern static void XkbSetDetectableAutoRepeat(IntPtr display, bool detectable, IntPtr supported);
        [DllImport(LIBNAME)]
        public extern static void XPeekEvent(IntPtr display, IntPtr xevent);
	}
}

