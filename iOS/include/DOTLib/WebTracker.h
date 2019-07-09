//
//  WebTracker.h
//  DOT
//
//  Created by Woncheol Heo on 2018. 9. 4..
//  Copyright © 2018년 wisetracker. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <WebKit/WebKit.h>

@interface WebTracker : NSObject

+ (WebTracker *)sharedInstance;

- (void)setWebView:(UIWebView *)webView request:(NSURLRequest *)request;
- (void)setWkWebView:(WKWebView *)wkWebView reqeust:(NSURLRequest *)request;
- (void)setPairingData:(UIWebView *)webView;
- (void)setPairingDataInWKWebView:(WKWebView *)wkWebView;
- (void)setPairingDataWithCookie:(NSArray *)cookie;
- (void)onStartWebPage;
- (double)onStopWebPage;
@end


